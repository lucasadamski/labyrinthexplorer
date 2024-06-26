﻿using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Models;
using LabyrinthExplorer.Logic.Models.GameElements;
using Event = LabyrinthExplorer.Logic.InternalCommunication.Event;
namespace LabyrinthExplorer.Logic.Managers.MenuManager
{
    internal class GameManager : Manager
    {
        public InputAction InputAction { get; set; }

        public UserPlayer? UserPlayer { get; set; } = new UserPlayer();
        public List<NPCPlayer> NPCPlayer { get; set; } = new List<NPCPlayer>();
        public Logger logger = new Logger();
        public InterActionDTO InterAction { get; set; } = new InterActionDTO();
        public GameElement[][] Map { get; set; }



        public override InternalDTO ReceiveInternalDTO(InternalDTO inputDTO)
        {
            if (inputDTO.InputAction == InputAction.ExitToMenu)
            {
                return QuitToGameEngineAndRequestEvent(Event.MenuMainPaused, inputDTO);
            }
            inputDTO.RequestUIInput = true;

            UserPlayer = inputDTO.UserPlayer;
            InputAction = inputDTO.InputAction;
            NPCPlayer = inputDTO.NPCPlayer;
            logger = inputDTO.Logger;
            Map = inputDTO.Map;


            InterAction = TranslateInputActionToInterAction(InputAction, UserPlayer.Position);
            InterAction = UserPlayer.ReceiveInterActionDTO(InterAction);

            logger.AppendDTOMessage(InterAction.DTO.Message);
            Map = ApplyInterActionDTOOnGameElementMap(Map, InterAction);

            if (NPCPlayer.Count() > 0)
            {
                NPCPlayer npc = NPCPlayer.ElementAt(0);
                bool npcAlive = !NPCPlayer.ElementAt(0).NotVisible;
                if (npcAlive)
                {
                    InterAction = TranslateInputActionToInterAction(InputAction, npc.Position);
                    InterAction = npc.ReceiveInterActionDTO(InterAction);
                    logger.AppendDTOMessage(InterAction.DTO.Message);
                    Map = ApplyInterActionDTOOnGameElementMap(Map, InterAction);
                }
            }

            if (UserPlayer.IsLevelFinished)
            {
                return QuitToGameEngineAndRequestEvent(Event.MenuLevelSummary, inputDTO);
            }
            if (UserPlayer.NotVisible) //if UserPlayer is dead request MenuGameOver
            {
                return QuitToGameEngineAndRequestEvent(Event.MenuGameOver, inputDTO);
            }

            return inputDTO;
        }

        private InternalDTO QuitToGameEngineAndRequestEvent(Event @event, InternalDTO inputDTO)
        {
            inputDTO.Event = @event;
            inputDTO.RequestUIInput = false;
            inputDTO.InputAction = InputAction.Unknown;
            return inputDTO;
        }

        public GameElement[][] ApplyInterActionDTOOnGameElementMap(GameElement[][] elementMap, InterActionDTO input)
        {
            try
            {
                elementMap[input.CenterPosition.X - 1][input.CenterPosition.Y - 1] = input.MapOfElements[0][0];
                elementMap[input.CenterPosition.X - 1][input.CenterPosition.Y] = input.MapOfElements[0][1];
                elementMap[input.CenterPosition.X - 1][input.CenterPosition.Y + 1] = input.MapOfElements[0][2];

                elementMap[input.CenterPosition.X][input.CenterPosition.Y - 1] = input.MapOfElements[1][0];
                elementMap[input.CenterPosition.X][input.CenterPosition.Y] = input.MapOfElements[1][1];
                elementMap[input.CenterPosition.X][input.CenterPosition.Y + 1] = input.MapOfElements[1][2];

                elementMap[input.CenterPosition.X + 1][input.CenterPosition.Y - 1] = input.MapOfElements[2][0];
                elementMap[input.CenterPosition.X + 1][input.CenterPosition.Y] = input.MapOfElements[2][1];
                elementMap[input.CenterPosition.X + 1][input.CenterPosition.Y + 1] = input.MapOfElements[2][2];

                logger.Log($"ApplyInterActionDTOOnGameElementMap: Success, Center Position X:{input.CenterPosition.X} Y:{input.CenterPosition.Y}");
                return elementMap;
            }
            catch (Exception e)
            {
                logger.LogError($"ApplyInterActionDTOOnGameElementMap: Error, Center Position X:{input.CenterPosition.X} Y:{input.CenterPosition.Y}");
                return elementMap;
            }
        }


        public InterActionDTO TranslateInputActionToInterAction(InputAction inputAction, Coordinates coordinates)
        {
            InterActionDTO interActionDTO = new InterActionDTO();
            interActionDTO.InputAction = inputAction;
            interActionDTO.CenterPosition = new Coordinates(coordinates.X, coordinates.Y);

            //Generate LocalMapOfElements
            try
            {
                interActionDTO.MapOfElements[0] = new GameElement[3];
                interActionDTO.MapOfElements[0][0] = Map[coordinates.X - 1][coordinates.Y - 1];
                interActionDTO.MapOfElements[0][1] = Map[coordinates.X - 1][coordinates.Y];
                interActionDTO.MapOfElements[0][2] = Map[coordinates.X - 1][coordinates.Y + 1];

                interActionDTO.MapOfElements[1] = new GameElement[3];
                interActionDTO.MapOfElements[1][0] = Map[coordinates.X][coordinates.Y - 1];
                interActionDTO.MapOfElements[1][1] = Map[coordinates.X][coordinates.Y];
                interActionDTO.MapOfElements[1][2] = Map[coordinates.X][coordinates.Y + 1];

                interActionDTO.MapOfElements[2] = new GameElement[3];
                interActionDTO.MapOfElements[2][0] = Map[coordinates.X + 1][coordinates.Y - 1];
                interActionDTO.MapOfElements[2][1] = Map[coordinates.X + 1][coordinates.Y];
                interActionDTO.MapOfElements[2][2] = Map[coordinates.X + 1][coordinates.Y + 1];
            }
            catch (Exception e)
            {
                logger.LogError($"TranslateInputActionToInterAction: Can't convert input {inputAction} and coordinates X:{coordinates.X} Y:{coordinates.Y} " +
                $"to InterActionDTO. Exception: {e.Message}");
                return new InterActionDTO();
            }

            logger.Log($"TranslateInputActionToInterAction: Converted input {inputAction} and coordinates X:{coordinates.X} Y:{coordinates.Y} " +
                $"to InterActionDTO");
            return interActionDTO;
        }
        public void ApplyCheats(string cheats)
        {
            if (cheats.Contains("give_all"))
            {
                UserPlayer.Pickup(new Key());
                UserPlayer.Pickup(new Weapon());
            }
            if (cheats.Contains("restore_health"))
            {
                UserPlayer.Health = 100;
            }
        }

    }
}

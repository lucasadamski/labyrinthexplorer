using LabyrinthExplorer.Logic;
using LabyrinthExplorer.Logic.Models.GameElements;
using System.ComponentModel;
using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using System.Linq;
using LabyrinthExplorer.Logic.DTOs;
using NuGet.Frameworks;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace LabyrinthExplorer.Test
{
    [TestClass]
    public class GameEngineTest
    {
        public IGlobalRepository repository { get; set; } = new GlobalRepository();
        [TestMethod]
        public void LoadLevelTest()
        {
            GameEngine GE = new GameEngine(Settings.TEST_LEVEL);

            Level testLevel = repository.GetLevel(Settings.TEST_LEVEL);


            char[][] map = new char[5][]
                 {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                 };
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Assert.AreEqual(map[i][j], GE.Canvas[i][j]);
                }
            }

        }

        /// <summary>
        /// Creates hardcoded 2D array, and compares each item if it's:
        /// 1) same type
        /// 2) same Coordinates
        /// </summary>
        [TestMethod]
        public void ParseCanvasToMapTest()
        {
            GameEngine GE = new GameEngine(Settings.TEST_LEVEL);
            GameElement[][] map = new GameElement[5][]
                 {
                          new GameElement[5] { new CornerWall(0,0) , new HorizontalWall(0, 1), new HorizontalWall(0, 2), new HorizontalWall(0, 3), new CornerWall(0, 4) }
                        , new GameElement[5] { new VerticalWall(1, 0), new UserPlayer(1, 1), new EmptySpace(1, 2), new EmptySpace(1, 3), new VerticalWall(1, 4) }
                        , new GameElement[5] { new VerticalWall(2, 0), new EmptySpace(2, 1), new EmptySpace(2, 2), new EmptySpace(2, 3), new VerticalWall(2, 4) }
                        , new GameElement[5] { new VerticalWall(3, 0), new EmptySpace(3, 1), new EmptySpace(3, 2), new EmptySpace(3, 3), new VerticalWall(3, 4) }
                        , new GameElement[5] { new CornerWall(4, 0), new HorizontalWall(4, 1), new HorizontalWall(4, 2), new HorizontalWall(4, 3), new CornerWall(4, 4) }
                 };

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Assert.AreEqual(map[i][j].GetType(), GE.Map[i][j].GetType());
                    Assert.AreEqual(map[i][j].Position.X, GE.Map[i][j].Position.X);
                    Assert.AreEqual(map[i][j].Position.Y, GE.Map[i][j].Position.Y);
                }
            }
        }

        [TestMethod]
        public void InitalizeUserPlayerTest()
        {
            //One Player
            char[][] map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };
            
            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            bool result = GE.logger.Message.ToString().Contains("InitializeUserPlayer: Found User Player at (1,1)");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //No Player
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeUserPlayer: Initialized User Player at (1,1)");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //3 Players
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', 'P', ' ', '|' }
                        , new char[5] { '|', ' ', 'P', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("ParseCanvasToMap: Found more than 1 User Player. User Player changed to EmptySpace.");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void InitalizeNPCPlayersTest()
        {
            //One NPC
            char[][] map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'E', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            bool result = GE.logger.Message.ToString().Contains("InitializeNPCPlayer: Found NPCPlayer 1 at (1,1)");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //No NPC
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeNPCPlayer: Added 0 NPC Players.");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //3 NPCs
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'E', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', 'E', ' ', '|' }
                        , new char[5] { '|', ' ', 'E', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeNPCPlayer: Added 3 NPC Players.");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void InitalizeInventoryTest()
        {
            //One Item
            char[][] map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', 'K', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            bool result = GE.logger.Message.ToString().Contains("InitializeInventory: Found 1 Items");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //No Item
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeInventory: Found 0 Items");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //3 Items
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', 'K', '|' }
                        , new char[5] { '|', ' ', 'W', ' ', '|' }
                        , new char[5] { '|', ' ', 'K', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeInventory: Found 3 Items");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void ReceiveInputDTOTest()
        {
            GameEngine GE = new GameEngine(Settings.TEST_LEVEL);
            GameEngineInputDTO GE_input = new GameEngineInputDTO();
            GE_input.InputAction = Logic.Models.InputAction.Up;
            GE.ReceiveInputDTO(GE_input);            
            bool result = GE.logger.Message.ToString().Contains("ReceiveInputDTO: Received InputAction: Up");
            Assert.AreEqual(GE.InputAction, GE_input.InputAction);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TranslateInputActionToInterActionTest()
        {
            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', 'K', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Unknown };

            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);

            bool result = GEoutput.Log.ToString().Contains("TranslateInputActionToInterAction: Converted input Unknown and coordinates X:1 Y:1 to InterActionDTO");
            Assert.AreEqual(true, result);
        }

        //PlayerTest
        [TestMethod]
        public void ReceiveInterActionDTOTest()
        {
            GameElement[][] map = new GameElement[3][]
                 {
                          new GameElement[3] { new CornerWall(0,0) , new HorizontalWall(0, 1), new HorizontalWall(0, 2) }
                        , new GameElement[3] { new VerticalWall(1, 0), new UserPlayer(1, 1), new EmptySpace(1, 2) }
                        , new GameElement[3] { new VerticalWall(2, 0), new EmptySpace(2, 1), new EmptySpace(2, 2) }
                 };
            InterActionDTO interActionDTO = new InterActionDTO();
            interActionDTO.MapOfElements = map;
            interActionDTO.InputAction = Logic.Models.InputAction.Down;
            UserPlayer up = new UserPlayer();
            up.Position = new Logic.Models.Coordinates(1, 1);
            up.Model = 'P';
            InterActionDTO output = up.ReceiveInterActionDTO(interActionDTO);

            Assert.AreEqual(typeof(UserPlayer), output.MapOfElements[2][1].GetType());
        }

        [TestMethod]
        public void RunEngineTest()
        {
            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', 'K', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };

            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);

            Assert.AreEqual('P', GEoutput.Frame[2][1]);
        }

        [TestMethod]
        public void GameEngineMoveDownTest()
        {
            /******************************************************************************************
             *          EMPTY_SPACE TEST
             ******************************************************************************************/

            //Move down to an Empty Space
            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', 'K', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[2][1]);

            /******************************************************************************************
             *          ITEM TESTS
             ******************************************************************************************/

            //Move down to an Key Item
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'K', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[2][1]);

            //Move down to a Weapon Item
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'W', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[2][1]);

            //1 Move down to a Trap Item
            //2 Player chagnes position one field down
            //3 Trap does damage to Player Settings.TRAP_DAMAGE (25 BY DEFAULT)
            //4 Trap dissapears
            //5 Player health should be 85, or Settings.PLAYER_FULL_HEALTH(100) - TRAP_DAMAGE(25)
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'X', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[2][1]);
            Assert.AreEqual(Settings.PLAYER_FULL_HEALTH - Settings.TRAP_DAMAGE, GE.UserPlayer.Health);


            /******************************************************************************************
             *          WALL TESTS
             ******************************************************************************************/

            //1 Move down to a Vartical Wall
            //2 Wall resists, stays in a place
            //3 Player stays in a place as well
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', '|', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('|', GEoutput.Frame[2][1]);

            //1 Move down to a Horizontal Wall
            //2 Wall resists, stays in a place
            //3 Player stays in a place as well
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', '-', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('-', GEoutput.Frame[2][1]);

            //1 Move down to a Corner Wall
            //2 Wall resists, stays in a place
            //3 Player stays in a place as well
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', '+', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('+', GEoutput.Frame[2][1]);

            /******************************************************************************************
             *          DOOR TESTS
             ******************************************************************************************/

            //1 Move down to a Closed Door
            //2 Door resists, stays in a place
            //3 Player stays in a place as well
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'D', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('D', GEoutput.Frame[2][1]);

            //1 Move down to a Opened Door
            //2 Player moves onto Door spot
            //3 Doors dissapear
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'O', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Assert.AreEqual('P', GEoutput.Frame[2][1]);


            /******************************************************************************************
             *          ENEMY TEST
             ******************************************************************************************/

            //1 Move down to a Enemy
            //2 Enemy resists, stays in a place
            //3 Player stays in a place as well
            //4 Player takes damage from Enemy
            //5 Check if health of Player lowered
            map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'E', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };
            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            //Trace.Write(GEoutput.Log);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('E', GEoutput.Frame[2][1]);
            Assert.AreEqual(Settings.PLAYER_FULL_HEALTH - Settings.ENEMY_DAMAGE, GE.UserPlayer.Health);
        }

        [TestMethod]
        public void UseWeaponTest()
        {
            //1 up uses weapon
            //2 npc receives damage 50
            //3 npc health is 100 - 50 = 50
            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'E', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.UseWeapon };

            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);

            Trace.Write(GEoutput.Log);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('E', GEoutput.Frame[2][1]);
            Assert.AreEqual(Settings.NPC_PLAYER_FULL_HEALTH - Settings.WEAPON_DAMAGE, GE.NPCPlayer.ElementAt(0).Health);
            
        }

        [TestMethod]
        public void OpenClosedDoorTest()
        {
            //1 player uses doors
            //2 doors change from closed to opened
            //3 check if doors are opened
            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'D', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Use };

            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);

            Trace.Write(GEoutput.Log);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('O', GEoutput.Frame[2][1]);
        }
        [TestMethod]
        public void CloseOpenedDoorTest()
        {
            //1 player uses doors
            //2 doors change from open to closed
            //3 check if doors are closed
            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'O', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Use };

            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);

            Trace.Write(GEoutput.Log);
            Assert.AreEqual('P', GEoutput.Frame[1][1]);
            Assert.AreEqual('D', GEoutput.Frame[2][1]);
        }

        [TestMethod]
        public void MoveThruOpenDoors()
        {
            //1 player takes two steps down : 1st onto the open doors 2nd onto empty space
            //2 doors should let thru the player
            //3 check if above player is opened door
            //4 check if abouve opened door is empty space
            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'O', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            //first step
            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);
            Trace.Write(GEoutput.Log);
            Assert.AreEqual(' ', GEoutput.Frame[1][1]);
            Assert.AreEqual('P', GEoutput.Frame[2][1]);
            Assert.AreEqual(' ', GEoutput.Frame[3][1]);
            //second step
            GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GEoutput = GE.RunEngine(GEinput);
            Trace.Write(GEoutput.Log);
            Assert.AreEqual(' ', GEoutput.Frame[1][1]);
            Assert.AreEqual('O', GEoutput.Frame[2][1]);
            Assert.AreEqual('P', GEoutput.Frame[3][1]);

        }

        [TestMethod]
        public void HUDTest()
        {
            /*              HUD
             * 
             *  1) UserPlayer Health
                2) UserPlayer Inventory
                3) Game Message:
                * UP interacted successfully with item, door, npc
                * UP cant open locked door
                * UP took damage
                * UP done damage
             * */

            //1 receive damage
            //message pops up
            //you have less health on health bar

            char[][] map = new char[5][]
               {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', 'X', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
               };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            //first step
            GameEngineInputDTO GEinput = new GameEngineInputDTO() { InputAction = Logic.Models.InputAction.Down };
            GameEngineOutputDTO GEoutput = GE.RunEngine(GEinput);
            Trace.Write(GEoutput.Log);
            Trace.Write(GEoutput.HUD);
            Assert.AreEqual(true , GEoutput.HUD.Contains("Health: 75"));
            Assert.AreEqual(true, GEoutput.HUD.Contains("Inventory:"));
            Assert.AreEqual(true, GEoutput.HUD.Contains("Message: User Player took 25 damage"));
            




            //2 do damage
            //3 pick up key
            //4 use locked doors without key
            //5 use doors with key


        }
    }
}
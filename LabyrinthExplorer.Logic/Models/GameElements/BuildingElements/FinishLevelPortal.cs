﻿using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements.BuildingElements
{
    public class FinishLevelPortal : Door
    {
        public FinishLevelPortal() : base()
        {
            Name = Settings.NAME_FINISH_LEVEL_PORTAL;
            Model = Settings.MODEL_FINISH_LEVEL_PORTAL;
            AlternateModel = Settings.MODEL_OPEN_DOOR;
        }

        public FinishLevelPortal (int x, int y, bool open = false, bool locked = false) : base(x, y, open, locked)
        {
            Name = Settings.NAME_FINISH_LEVEL_PORTAL;
            Model = Settings.MODEL_FINISH_LEVEL_PORTAL;
            AlternateModel = Settings.MODEL_OPEN_DOOR;
        }

        override public DTO ReceiveStep(CharacterElement player)
        {
            DTO output = new DTO();
            if (Open == true)
            {
                output.AppendDebugMessage($"{this.Name} is open. {player.Name} allowed to step in");
                output.AppendActionMessage(Settings.MESSAGE_LEVEL_FINISHED);
                return output;
            }
            else
            {
                output.Success = false;
                output.AppendDebugMessage($"{this.Name} is closed. {player.Name} not allowed to step in");
                return output;
            }
        }
    }
}
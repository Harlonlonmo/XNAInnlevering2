using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Innlevering_2
{
    public class MultiuplayerInput : GameComponent
    {

        public Dictionary<PlayerIndex, GamePadState> GamePadStates { get; protected set; }


        public MultiuplayerInput(Game game)
            : base(game)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }

    
}

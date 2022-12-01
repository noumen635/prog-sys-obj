﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class Chef : MotionkitchenItem
    {
        private static string spritePath = "Resources/Chef/";

        private static string imageFront = "front.png";
        private static string imageBack = "back.png";
        private static string imageLeft = "left.png";
        private static string imageRight = "right.png";
        private static string imageStop = "stop.png";

        private Sprite front = new Sprite(spritePath, imageFront);
        private Sprite back = new Sprite(spritePath, imageBack);
        private Sprite left = new Sprite(spritePath, imageLeft);
        private Sprite right = new Sprite(spritePath, imageRight);
        private Sprite stop = new Sprite(spritePath, imageStop);

        public Chef()
        {
            this.posX = 0;
            this.posY = 0;

            //front.loadImage();
            //back.loadImage();
            //left.loadImage();
            //right.loadImage();
            //stop.loadImage();

            this.setSprite(front);
        }
    }
}

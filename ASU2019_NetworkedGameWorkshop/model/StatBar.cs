﻿using ASU2019_NetworkedGameWorkshop.model.character;
using ASU2019_NetworkedGameWorkshop.model.grid;
using System.Drawing;

namespace ASU2019_NetworkedGameWorkshop.model
{
    public class StatBar : GraphicsObject
    {
        private const float WIDTH = Tile.WIDTH - 30 * 2,
            WIDTH_HALF = WIDTH / 2f,
            Height = 8;
        private const float HEX_OFFSET_Y = Tile.HEIGHT * 0.2f - 20;
        private const int BACK_PADDING_H = 6;
        private const float BACK_OFFSET_X = WIDTH_HALF + BACK_PADDING_H / 2f,
            BACK_OFFSET_Y = 2f;
        private const float BACK_WIDTH = WIDTH + BACK_PADDING_H,
            BACK_HEIGHT = Height + BACK_OFFSET_Y;

        private static readonly Brush BACK_BRUSH = new SolidBrush(Color.FromArgb(150, 0, 0, 0));
        private static readonly Font DEBUG_FONT = new Font("Roboto", 8f);

        private readonly Brush brush;
        private readonly Character character;
        private readonly float offsetY;
        private readonly float backOffsetY;
        private int trackedStat;
        private int trackedStatMax;

        public StatBar(Character character, Brush brush, int order)
        {
            this.character = character;
            this.brush = brush;
            offsetY = -Tile.HALF_HEIGHT + order * BACK_HEIGHT + HEX_OFFSET_Y;
            backOffsetY = offsetY - (BACK_OFFSET_Y / 2f);
        }

        public override void draw(Graphics graphics)
        {
            graphics.FillRectangle(BACK_BRUSH,
                character.CurrentTile.centerX - BACK_OFFSET_X,
                character.CurrentTile.centerY + backOffsetY,
                BACK_WIDTH,
                BACK_HEIGHT);
            graphics.FillRectangle(brush,
                character.CurrentTile.centerX - WIDTH_HALF,
                character.CurrentTile.centerY + offsetY,
                WIDTH * trackedStat / trackedStatMax,
                Height);
        }

        /// <summary>
        /// Draws a string containing the Maximum and current value of the StatBar.
        /// </summary>
        /// <param name="graphics">graphics object to draw on.</param>
        public override void drawDebug(Graphics graphics)
        {
            graphics.DrawString(string.Format("{0}/{1}", trackedStat, trackedStatMax),
                DEBUG_FONT, Brushes.White,
                character.CurrentTile.centerX - WIDTH_HALF,
                character.CurrentTile.centerY + offsetY);
        }

        /// <summary>
        /// updates the Tracked Stat value then calls Draw().
        /// </summary>
        /// <param name="graphics">graphics object to draw on.</param>
        /// <param name="trackedStat">updated value of trackedStat.</param>
        /// <param name="trackedStatMax">updated value of trackedStatMax.</param>
        public void updateTrackedAndDraw(Graphics graphics, int trackedStat, int trackedStatMax)
        {
            this.trackedStat = trackedStat;
            this.trackedStatMax = trackedStatMax;
            draw(graphics);
        }
    }
}

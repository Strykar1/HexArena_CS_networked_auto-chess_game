﻿using ASU2019_NetworkedGameWorkshop.model.grid;
using System;
using System.Windows.Forms;

namespace ASU2019_NetworkedGameWorkshop.controller {
    class GameManager {
        private Timer timer;
        private readonly Grid grid;
        private Tile selectedTile;
        private readonly GameForm gameForm;

        public GameManager(GameForm gameForm) {
            grid = new Grid(10, 8, 10, 10);
            this.gameForm = gameForm;
        }
        public void startTimer() {
            timer = new Timer();
            timer.Interval = 50; //Arbitrary: 20 ticks per sec
            timer.Tick += new EventHandler(gameLoop);
            timer.Start();
        }

        public void updatePaint(PaintEventArgs e) {
            grid.draw(e.Graphics);
        }

        internal void mouseClick(MouseEventArgs e) {
            Tile tile = grid.getSelectedHexagon(e.X, e.Y);
            if(tile != null) {
                Console.WriteLine("Tile selected: ({0}, {1})", tile.X, tile.Y);//Debugging

                if(selectedTile != null) {
                    selectedTile.Selected = false;
                }
                tile.Selected = true;
                selectedTile = tile;

                gameForm.Invalidate();
            }
        }

        private void gameLoop(object sender, EventArgs e) {

        }
    }
}

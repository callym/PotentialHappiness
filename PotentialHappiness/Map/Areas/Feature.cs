﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Map.Cells;

namespace PotentialHappiness.Map.Areas
{
	public class Feature
	{
		public TileMap Map;
		List<MapCell> _cells;
		public List<MapCell> Cells
		{
			get
			{
				return new List<MapCell>(_cells);
			}
			private set
			{
				_cells = value;
			}
		}
		public Color Color = Color.White;
		public int Priority = 1;

		public Feature(TileMap map)
		{
			Map = map;
			Cells = new List<MapCell>();
		}

		public bool Contains(int x, int y)
		{
			for (int i = 0; i < Cells.Count; i++)
			{
				MapCell c = Cells[i];
				if (c.X == x && c.Y == y)
				{
					return true;
				}
			}
			return false;
		}

		public void Add(MapCell cell)
		{
			if ((cell.Feature?.Priority ?? 0) < this.Priority)
			{
				cell.Feature = this;
			}
			Cells.Add(cell);
		}

		public void Add(List<MapCell> cells)
		{
			cells.ForEach((c) => Add(c));
		}

		public void Remove(MapCell cell)
		{
			cell.Feature = null;
			_cells.Remove(cell);
		}

		public void Clear()
		{
			_cells.ForEach((c) => Remove(c));
			_cells.Clear();
		}
	}
}

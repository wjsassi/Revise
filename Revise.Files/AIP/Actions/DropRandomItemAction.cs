﻿#region License

/**
 * Copyright (C) 2012 Jack Wakefield
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#endregion

using System.IO;

namespace Revise.Files {
    /// <summary>
    /// Represents an action to drop a single item from the range specified.
    /// </summary>
    public class DropRandomItemAction : IArtificialIntelligenceAction {
        public const int ITEM_COUNT = 5;

        #region Properties

        /// <summary>
        /// Gets the action type.
        /// </summary>
        public ArtificialIntelligenceActions Type {
            get {
                return ArtificialIntelligenceActions.DropRandomItem;
            }
        }

        /// <summary>
        /// Gets or sets the range of items to drop.
        /// </summary>
        public short[] Items {
            get;
            private set;
        }
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DropRandomItemAction"/> class.
        /// </summary>
        public DropRandomItemAction() {
            Items = new short[ITEM_COUNT];
        }

        /// <summary>
        /// Reads the condition data from the underlying stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(BinaryReader reader) {
            for (int i = 0; i < Items.Length; i++) {
                Items[i] = reader.ReadInt16();
            }
        }

        /// <summary>
        /// Writes the condition data to the underlying stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer) {
            for (int i = 0; i < Items.Length; i++) {
                writer.Write(Items[i]);
            }
        }
    }
}
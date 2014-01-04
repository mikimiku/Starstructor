/*Starstructor, the Starbound Toolet 
Copyright (C) 2013-2014 Chris Stamford
Contact: cstamford@gmail.com

Source file contributers:
 Chris Stamford     contact: cstamford@gmail.com
 Adam Heinermann    contact: aheinerm@gmail.com

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using DungeonEditor.EditorObjects;
using Newtonsoft.Json;
using System.ComponentModel;
using DungeonEditor.EditorTypes;

namespace DungeonEditor.StarboundObjects.Objects
{
    [ReadOnly(true)]
    public class ObjectOrientation
    {
        [JsonIgnore] 
        public ObjectFrames LeftFrames;

        [JsonIgnore]
        public Image LeftImage;

        [JsonIgnore] 
        public ObjectFrames RightFrames;

        [JsonIgnore] 
        public Image RightImage;

        [JsonProperty("image")]
        public string ImageName { get; set; }

        [JsonProperty("dualImage")]
        public string DualImageName { get; set; }

        // Only available if dualImage is true
        [JsonProperty("leftImage")]
        public string LeftImageName { get; set; }

        // Only available if dualImage is true
        [JsonProperty("rightImage")]
        public string RightImageName { get; set; }

        [JsonProperty("imageLayers")]
        public List<ObjectImageLayer> ImageLayers { get; set; }

        // only if imageLayers is not found
        [JsonProperty("unlit")]
        [DefaultValue(false)]
        public bool? Unlit { get; set; }

        [JsonProperty("flipImages")]
        [DefaultValue(false)]
        public bool? FlipImages { get; set; }

        // Default: 0,0
        // Vec2F, so should be double
        [JsonProperty("imagePosition"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Vec2F ImagePosition { get; set; }
        //public List<double> ImagePosition { get; set; }

        [JsonProperty("frames")]
        [DefaultValue(1)]
        public int? AnimFramesCount { get; set; }

        [JsonProperty("animationCycle")]
        [DefaultValue(1.0)]
        public double? AnimationCycle { get; set; }

        // List<Vec2I>
        [JsonProperty("spaces")]
        public BindingList<Vec2I> Spaces { get; set; }

        [JsonProperty("spaceScan")]
        public double? SpaceScan { get; set; }

        [JsonProperty("requireTilledAnchors")]
        [DefaultValue(false)]
        public bool? RequireTilledAnchors { get; set; }

        [JsonProperty("requireSoilAnchors")]
        [DefaultValue(false)]
        public bool? RequireSoilAnchors { get; set; }

        // Contains "left", "bottom", "right", "top", "background"
        [JsonProperty("anchors")]
        public List<string> Anchors { get; set; }

        // List<Vec2I>
        [JsonProperty("bgAnchors")]
        public BindingList<Vec2I> BackgroundAnchors { get; set; }

        // List<Vec2I>
        [JsonProperty("fgAnchors")]
        public BindingList<Vec2I> ForegroundAnchors { get; set; }

        // either "left" or "right", defaults to right if rightImage
        [JsonProperty("direction")]
        [DefaultValue("left")]
        public string Direction { get; set; }

        // either "none", "solid", or "platform"
        [JsonProperty("collision")]
        [DefaultValue("none")]
        public string Collision { get; set; }

        // Vec2F
        [JsonProperty("lightPosition"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Vec2F LightPosition { get; set; }
        //public List<double> LightPosition { get; set; }

        [JsonProperty("pointAngle")]
        [DefaultValue(0.0)]
        public double PointAngle { get; set; }

        //particleEmitter       optional, object with more properties
        //particleEmitters      optional, list of particleEmitter

        private double GetSizeScaleFactor(double gridFactor)
        {
            return Editor.DEFAULT_GRID_FACTOR / gridFactor;
        }

        private ObjectFrames GetFrames(ObjectDirection direction)
        {
            ObjectFrames frames = null;
            if (direction == ObjectDirection.DIRECTION_LEFT && LeftFrames != null)
                frames = LeftFrames;
            else
                frames = RightFrames;
            return frames;
        }

        public int GetWidth(int gridFactor = Editor.Editor.DEFAULT_GRID_FACTOR, ObjectDirection direction = ObjectDirection.DIRECTION_NONE)
        {
            var sizeScaleFactor = GetSizeScaleFactor(gridFactor);
            ObjectFrames frames = GetFrames(direction);
            return (int) Math.Ceiling(frames.FrameGrid.Size.x/sizeScaleFactor);
        }

        public int GetHeight(int gridFactor = Editor.Editor.DEFAULT_GRID_FACTOR, ObjectDirection direction = ObjectDirection.DIRECTION_NONE)
        {
            var sizeScaleFactor = GetSizeScaleFactor(gridFactor);

            ObjectFrames frames = GetFrames(direction);
            return (int) Math.Ceiling(frames.FrameGrid.Size.y/sizeScaleFactor);
        }

        public int GetOriginX(int gridFactor = Editor.Editor.DEFAULT_GRID_FACTOR, ObjectDirection direction = ObjectDirection.DIRECTION_NONE)
        {
            var sizeScaleFactor = GetSizeScaleFactor(gridFactor);

            int originX = 0;
            originX += (int) Math.Floor(ImagePosition.x/sizeScaleFactor);

            return originX;
        }

        public int GetOriginY(int gridFactor = Editor.Editor.DEFAULT_GRID_FACTOR, ObjectDirection direction = ObjectDirection.DIRECTION_NONE)
        {
            var sizeScaleFactor = GetSizeScaleFactor(gridFactor);

            int originY = -GetHeight(gridFactor, direction) + gridFactor;
            originY -= (int) Math.Floor(ImagePosition.y/sizeScaleFactor);

            return originY;
        }
    }
}
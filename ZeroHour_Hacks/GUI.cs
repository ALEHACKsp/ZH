using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using UnityEngine;
using CustomTypes;

namespace _GUI
{
    public static class m_GUI
    {
        public static Color buttonColor = new Color(0.9f, 0.9f, 0.9f);
        public static Color toggleColorOn = new Color(0.3f, 0.9f, 0.3f);
        public static Color toggleColorOff = new Color(0.9f, 0.3f, 0.3f);

        public static int windowHorizontalBuffer = 10;
        public static int windowStackBuffer = 20;
        public static int buttonWidth = 150;
        public static int buttonHeight = 20;
        public static void makeButton(Action operation, String text, int stackNo)
        {
            GUI.color = buttonColor;
            Vector2 pos = new Vector2(windowHorizontalBuffer, windowStackBuffer * stackNo);
            Vector2 size = new Vector2(buttonWidth, buttonHeight);

            if (GUI.Button(new Rect(pos, size), text))
            {
                operation();
            }
        }
        public static void makeLabel(String text, int stackNo)
        {
            GUI.color = buttonColor;
            Vector2 pos = new Vector2(windowHorizontalBuffer, windowStackBuffer * stackNo);
            Vector2 size = new Vector2(buttonWidth, buttonHeight);
            GUI.Label(new Rect(pos, size), text);
        }
        public static float makeSlider(float item, float left, float right, int stackNo, bool controller, bool roundValue = false)
        {

            GUI.color = buttonColor;
            Vector2 pos = new Vector2(windowHorizontalBuffer, windowStackBuffer * stackNo);
            Vector2 size = new Vector2(buttonWidth, buttonHeight);

            float result = item;
            return GUI.HorizontalSlider(new Rect(pos, size), item, left, right);

        }

        public static float makeSlider(float item, float left, float right, int stackNo)
        {

            GUI.color = buttonColor;
            Vector2 pos = new Vector2(windowHorizontalBuffer, windowStackBuffer * stackNo);
            Vector2 size = new Vector2(buttonWidth, buttonHeight);

            float result = item;
            return GUI.HorizontalSlider(new Rect(pos, size), item, left, right);

        }

        public static bool makeCheckbox(bool item, String text, int stackNo, bool dependent = false, bool dependancy = true)
        {
            if (item == true)
            {
                GUI.color = toggleColorOn;
            }
            else
            {
                GUI.color = toggleColorOff;
            }

            if ((dependent && dependancy == true) || !dependent)
            {
                Vector2 pos = new Vector2(windowHorizontalBuffer, windowStackBuffer * stackNo);
                Vector2 size = new Vector2(buttonWidth, buttonHeight);
                return GUI.Toggle(new Rect(pos, size), item, text);

            }
            else if (dependent && dependancy == false)
            {
                Vector2 pos = new Vector2(windowHorizontalBuffer, windowStackBuffer * stackNo);
                Vector2 size = new Vector2(buttonWidth, buttonHeight);
                GUI.Label(new Rect(pos, size), "  - " + text);
                return false;
            }
            return false;

        }


        public static void DrawBox(Vector2 position, Vector2 size, float thickness, bool centered = true)
        {
            var upperLeft = centered ? position - size / 2f : position;
            GUI.DrawTexture(new Rect(position.x, position.y, size.x, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y, thickness, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x + size.x, position.y, thickness, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y + size.y, size.x + thickness, thickness), Texture2D.whiteTexture);
        }


        public static void DrawBoxFill(Vector2 position, Vector2 size, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, ScaleMode.StretchToFill);
        }


        public static void DrawCircle(Vector2 position, float radius, int numSides, Color color, bool centered = true, float thickness = 1f)
        {
            RingArray arr;
            if (ringDict.ContainsKey(numSides))
                arr = ringDict[numSides];
            else
                arr = ringDict[numSides] = new RingArray(numSides);


            var center = centered ? position : position + Vector2.one * radius;

            for (int i = 0; i < numSides - 1; i++)
                DrawLine(center + arr.Positions[i] * radius, center + arr.Positions[i + 1] * radius, thickness, color);

            DrawLine(center + arr.Positions[0] * radius, center + arr.Positions[arr.Positions.Length - 1] * radius, thickness, color);
        }
        public static void DrawLine(Vector2 from, Vector2 to, float thickness, Color color)
        {
            GUI.color = color;
            DrawLine(from, to, thickness);
        }
        public static void DrawLine(Vector2 from, Vector2 to, float thickness)
        {
            var delta = (to - from).normalized;
            var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            GUIUtility.RotateAroundPivot(angle, from);
            DrawBox(from, Vector2.right * (from - to).magnitude, thickness, false);
            GUIUtility.RotateAroundPivot(-angle, from);
        }
        private class RingArray
        {
            public Vector2[] Positions { get; private set; }

            public RingArray(int numSegments)
            {
                Positions = new Vector2[numSegments];
                var stepSize = 360f / numSegments;
                for (int i = 0; i < numSegments; i++)
                {
                    var rad = Mathf.Deg2Rad * stepSize * i;
                    Positions[i] = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
                }
            }
        }

        private static Dictionary<int, RingArray> ringDict = new Dictionary<int, RingArray>();


        public static void setDefaultskin()
        {
            GUISkin skin = GUI.skin;
            GUIStyle newStyle = skin.GetStyle("Label");
            newStyle.fontSize = 14;
            GUI.color = Color.white;
        }

        public static void setSolidSkin()
        {
            GUISkin skin = GUI.skin;
            GUIStyle newStyle = skin.GetStyle("Label");
            newStyle.fontSize = 14;
            GUI.color = Color.white;
        }
    }
    
}

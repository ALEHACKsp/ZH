using System;
using System.Collections.Generic;
using UnityEngine;

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

        public static void makeButton(Action operation, string text, int stackNo)
        {
            GUI.color = buttonColor;
            Vector2 pos = new Vector2(windowHorizontalBuffer, windowStackBuffer * stackNo);
            Vector2 size = new Vector2(buttonWidth, buttonHeight);

            if (GUI.Button(new Rect(pos, size), text))
            {
                operation();
            }
        }

        public static void _DrawLine(Vector2 start, Vector2 end, int width)
        {
            Vector2 d = end - start;
            float a = Mathf.Rad2Deg * Mathf.Atan(d.y / d.x);
            if (d.x < 0)
            {
                a += 180;
            }

            int width2 = (int)Mathf.Ceil(width / 2);

            GUIUtility.RotateAroundPivot(a, start);
            GUI.DrawTexture(new Rect(start.x, start.y - width2, d.magnitude, width), Texture2D.whiteTexture);
            GUIUtility.RotateAroundPivot(-a, start);
        }
        public static bool MenuWindowSwitch(bool item, string text, Vector2 Position)
        {
            if (GUI.Button(new Rect(Position.x, Position.y, m_GUI.buttonWidth + (windowHorizontalBuffer * 2), m_GUI.buttonHeight * 1.3f), text))
            {
                return !item;
            }
            return item;
        }

        public static void makeLabel(string text, int stackNo)
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

        public static bool makeCheckbox(bool item, string text, int stackNo, bool dependent = false, bool dependancy = true)
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
                return item;
            }
            return false;
        }


        public static void DrawBox(Vector2 position, Vector2 size, float thickness, bool centered = true)
        {
            Vector2 upperLeft = centered ? position - size / 2f : position;
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
            {
                arr = ringDict[numSides];
            }
            else
            {
                arr = ringDict[numSides] = new RingArray(numSides);
            }

            Vector2 center = centered ? position : position + Vector2.one * radius;

            for (int i = 0; i < numSides - 1; i++)
            {
                DrawLine(center + arr.Positions[i] * radius, center + arr.Positions[i + 1] * radius, thickness, color);
            }

            DrawLine(center + arr.Positions[0] * radius, center + arr.Positions[arr.Positions.Length - 1] * radius, thickness, color);
        }
        public static void DrawLine(Vector2 from, Vector2 to, float thickness, Color color)
        {
            GUI.color = color;
            DrawLine(from, to, thickness);
        }
        public static void DrawLine(Vector2 from, Vector2 to, float thickness)
        {
            Vector2 delta = (to - from).normalized;
            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
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
                float stepSize = 360f / numSegments;
                for (int i = 0; i < numSegments; i++)
                {
                    float rad = Mathf.Deg2Rad * stepSize * i;
                    Positions[i] = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
                }
            }
        }

        private static readonly Dictionary<int, RingArray> ringDict = new Dictionary<int, RingArray>();


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

        public class dropDown
        {
            public Vector2 scrollViewVector = Vector2.zero;

            public string[] list;

            public int indexNumber;
            public bool show = false;
            public static int buttonWidth;
            public static int buttonHeight;

            public string selection;
            public dropDown(string[] options, int width, int height)
            {
                list = options;
                buttonHeight = height;
                buttonWidth = width;
                selection = options[0];
            }
            public void makeDropper(int stackNo)
            {
                GUI.color = Color.white;
                Rect dropDownRect = new Rect(10 + buttonWidth, buttonHeight * stackNo, buttonWidth, buttonWidth * 2);


                if (GUI.Button(new Rect((dropDownRect.x - buttonWidth), dropDownRect.y, dropDownRect.width, buttonHeight), ""))
                {
                    if (!show)
                    {
                        show = true;
                    }
                    else
                    {
                        show = false;
                    }
                }

                if (show)
                {
                    scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - buttonWidth),
                        (dropDownRect.y + buttonHeight), dropDownRect.width, dropDownRect.height),
                        scrollViewVector, new Rect(0, 0, buttonWidth, Mathf.Max(dropDownRect.height,
                        (list.Length * buttonHeight))));

                    GUI.Box(new Rect(0, 0, buttonWidth, (list.Length * buttonHeight)), "");

                    for (int index = 0; index < list.Length; index++)
                    {

                        if (GUI.Button(new Rect(0, (index * buttonHeight), buttonWidth, buttonHeight), ""))
                        {
                            show = false;
                            indexNumber = index;
                            selection = list[indexNumber];
                        }

                        GUI.Label(new Rect(10, (index * buttonHeight) - 1, buttonWidth, buttonHeight), list[index]);

                    }

                    GUI.EndScrollView();
                }
                else
                {
                    GUI.Label(new Rect(10 + (dropDownRect.x - buttonWidth), dropDownRect.y - 1, buttonWidth, buttonHeight), list[indexNumber]);
                }
            }

        }






    }

}

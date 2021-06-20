using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Balance;
using Map.Generate;

namespace Tools
{
    public class ClusterDesigner : EditorWindow
    {
        private int length = 5;

        private int lines = 9;

        private static MapBalance.SegmentConfig segmentConfig = new MapBalance.SegmentConfig();

        private MapSegment segment = null;

        private Vector2 scroll = Vector2.zero;

        string lineSeparator = ";";
        string symbolSeparator = ",";

        [MenuItem("Window/Cluster Designer")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ClusterDesigner));
            segmentConfig = new MapBalance.SegmentConfig();
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Base Settings", EditorStyles.boldLabel);

            segment = (MapSegment) EditorGUILayout.ObjectField("Segment", segment, typeof(MapSegment), true);

            EditorGUILayout.BeginHorizontal();

            length = EditorGUILayout.IntField("Length", length);
            EditorGUILayout.TextField("Result", ParceLine());
            lineSeparator = EditorGUILayout.TextField("Line separator", lineSeparator, GUILayout.Width(250f));
            symbolSeparator = EditorGUILayout.TextField("Symbol separator", symbolSeparator, GUILayout.Width(250f));

            EditorGUILayout.EndHorizontal();

            if(segmentConfig.spawnConfig == null || segmentConfig.spawnConfig.Length == 0 || length != segmentConfig.spawnConfig[0].indexses.Length)
            {
                MapBalance.SegmentConfig temp_rows = new MapBalance.SegmentConfig();
                temp_rows.spawnConfig = new MapBalance.ClusterRow[lines];
                temp_rows.parameters = new MapBalance.ClusterRow[lines];

                PrepareRowData(temp_rows);

                segmentConfig = temp_rows;
            }

            scroll = EditorGUILayout.BeginScrollView(scroll);

            EditorGUILayout.LabelField("Indexes");
            GenerateGrid(segmentConfig.spawnConfig);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Parameters");
            GenerateGrid(segmentConfig.parameters);

            EditorGUILayout.EndScrollView();
        }


        private void PrepareRowData(MapBalance.SegmentConfig temp_rows)
        {
            for (int r = 0; r < lines; ++r)
            {
                temp_rows.spawnConfig[r] = new MapBalance.ClusterRow();
                temp_rows.parameters[r] = new MapBalance.ClusterRow();
                temp_rows.spawnConfig[r].indexses = new int[length];
                temp_rows.parameters[r].indexses = new int[length];

                for (int i = 0; i < length; ++i)
                {
                    if (segmentConfig.spawnConfig != null &&
                        segmentConfig.spawnConfig.Length > 0 &&
                        segmentConfig.spawnConfig[r] != null &&
                        segmentConfig.spawnConfig[r].indexses != null &&
                        segmentConfig.spawnConfig[r].indexses.Length > i)
                    {
                        temp_rows.spawnConfig[r].indexses[i] = segmentConfig.spawnConfig[r].indexses[i];
                    }
                    else
                    {
                        temp_rows.spawnConfig[r].indexses[i] = 0;
                    }

                    if (segmentConfig.parameters != null &&
                        segmentConfig.parameters.Length > 0 &&
                        segmentConfig.parameters[r] != null &&
                        segmentConfig.parameters[r].indexses != null &&
                        segmentConfig.parameters[r].indexses.Length > i)
                    {
                        temp_rows.parameters[r].indexses[i] = segmentConfig.parameters[r].indexses[i];
                    }
                    else
                    {
                        temp_rows.parameters[r].indexses[i] = 0;
                    }
                }
            }

            if (segment != null)
                segment.NextSegment(segmentConfig);
        }

        private void GenerateGrid(MapBalance.ClusterRow[] rows)
        {
            int rowIndex = 0;

            EditorGUILayout.BeginVertical();
            for (int r = 0; r < lines; ++r)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(r.ToString(), GUILayout.Width(20f));
                for (int i = 0; i < length; ++i)
                {
                    int newVal = EditorGUILayout.IntField(rows[r].indexses[i]);
                    if (newVal != rows[r].indexses[i])
                    {
                        rows[r].indexses[i] = newVal;
                        if (segment != null)
                            segment.NextSegment(segmentConfig);

                    }
                }
                EditorGUILayout.EndHorizontal();

                if (rowIndex == 2 || rowIndex == 5)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                }

                ++rowIndex;
            }
            EditorGUILayout.EndVertical();
        }

        private string ParceLine()
        {
            if (segmentConfig == null || segmentConfig.spawnConfig == null || segmentConfig.parameters == null)
                return "";

            string result = "";
            result = result + "indexes" + lineSeparator;

            foreach (var row in segmentConfig.spawnConfig)
            {
                foreach (var item in row.indexses)
                {
                    result = result + symbolSeparator + item.ToString();
                }
                result = result + lineSeparator;
            }

            result = result + "parameters" + lineSeparator;

            foreach (var row in segmentConfig.parameters)
            {
                foreach (var item in row.indexses)
                {
                    result = result + symbolSeparator + item.ToString();
                }
                result = result + lineSeparator;
            }

            return result;
        }
    }
}

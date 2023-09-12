using System;
using System.IO;
using System.Linq;
using ModelViewing;
using Survey.Handler;
using UnityEngine;

namespace Survey {
    public class SurveySerializer : MonoBehaviour {
        
        [SerializeField] private ModelRowManager _manager;
        [SerializeField] private QuestionSpawner _handler;

        private StreamWriter _writer;
        private string _currentData = null;
        private void Awake() {
            // since we separate values with a comma, setup that floats use dots instead
            System.Globalization.CultureInfo customCulture =
                (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            
            string path = Application.dataPath + "/Data/";
            Directory.CreateDirectory(path);
            _writer = new StreamWriter($"{path}{GetFullTimeStamp()}.txt");
        }
        
        private void OnDestroy() => _writer.Close();
        
        public static string GetFullTimeStamp() => DateTime.Now.ToString("dd-MM-yy_hh-mm-ss");

        private void OnEnable() {
            _handler.OnHandlersSpawned += HandleHandlersSpawned;
            _manager.OnFinish += WriteData;
        }

        private void OnDisable() {
            _handler.OnHandlersSpawned -= HandleHandlersSpawned;
            _manager.OnFinish += WriteData;
        }

        private void HandleHandlersSpawned() {
            WriteData();
            foreach (SurveyHandler handler in _handler.Handlers) {
                handler.OnValueChanged += Reevaluate;
            }
            Reevaluate();
        }
        
        private void WriteData() {
            if (_currentData == null) return;
            _writer.WriteLine(_currentData);
            _writer.Flush();
            _currentData = null;
        }

        private void Reevaluate() {
            _currentData = _handler.Title + "\n\n" +
                string.Join("\n", _handler.Handlers
                    .Select(handler => handler.Question + "\n" + handler.ExtractEnteredData()))
                + "\n\n";
        }
        
    }
}
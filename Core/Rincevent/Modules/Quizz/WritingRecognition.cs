using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Ink;
using Microsoft.StylusInput;

namespace Meow.FR.Rincevent.Display.Quizz
{
    /// <summary>
    /// Writing recognition class.
    /// </summary>
    public class WritingRecognition
    {
        private readonly InkOverlay _inkOverlay;
        private readonly RecognizerContext _context;
        private readonly Strokes _strokes;
        private readonly Recognizers _recognizers = new Recognizers();

        public class HanziRecognizedEventArgs
        {
            public string BestResult;
            public List<string> AlternativeResults;
        }

        public InkOverlayEditingMode EditingMode
        {
            get
            {
                return _inkOverlay.EditingMode;
            }
            set
            {
                _inkOverlay.EditingMode = value;
            }
        }

        private Color _FirstColor = Color.DodgerBlue;

        public Color FirstColor
        {
            get { return _FirstColor; }
            set { _FirstColor = value; }
        }

        private Color _SecondColor = Color.DarkViolet;

        public Color SecondColor
        {
            get { return _SecondColor; }
            set { _SecondColor = value; }
        }

        public delegate void HanziRecognizedHandler(object sender, HanziRecognizedEventArgs e);
        public event HanziRecognizedHandler HanziRecognized;

        /// <summary>
        /// Gets a value indicating whether recognition is installed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if recognition is installed; otherwise, <c>false</c>.
        /// </value>
        static public bool RecognitionIsInstalled
        {
            get
            {
                try
                {
                    Recognizers recognizers = new Recognizers();
                    if (recognizers.Count > 0)
                        using (new GestureRecognizer()) 
                            return true;
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WritingRecognition"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="culture">The culture.</param>
        public WritingRecognition(Control control, CultureInfo culture)
        {
            foreach (Recognizer reco in _recognizers)
                for (int c = 0; c < reco.Languages.Length; c++)
                    if (reco.Languages[c] == culture.LCID)
                    {
                        _context = reco.CreateRecognizerContext();
                        break;
                    }
            if (_context == null)
            {
                MessageBox.Show(culture.DisplayName + " handwriting recognition support isn't installed on this Windows.", "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            _inkOverlay = new InkOverlay(control, true);
            _inkOverlay.Enabled = true;
            _strokes = _inkOverlay.Ink.CreateStrokes();
            _context.Strokes = _strokes;
            _inkOverlay.Stroke += inkOverlay_Stroke;
            _inkOverlay.DefaultDrawingAttributes.AntiAliased = true;
            _inkOverlay.DefaultDrawingAttributes.Color = Color.DarkViolet;
            _inkOverlay.DefaultDrawingAttributes.FitToCurve = true;
            _context.RecognitionWithAlternates += context_RecognitionWithAlternates;
        }

        private void OnThread_RecognitionWithAlternates(object sender, HanziRecognizedEventArgs e)
        {
            HanziRecognized.Invoke(this, e);
        }

        private void context_RecognitionWithAlternates(object sender, RecognizerContextRecognitionWithAlternatesEventArgs e)
        {
            if (_inkOverlay.AttachedControl.InvokeRequired)
            {
                HanziRecognizedEventArgs ev = new HanziRecognizedEventArgs();
                ev.BestResult = e.Result.TopString;
                ev.AlternativeResults = new List<string>();
                RecognitionAlternates alternates = e.Result.GetAlternatesFromSelection(0, e.Result.TopString.Length);
                foreach (RecognitionAlternate alternate in alternates)
                    ev.AlternativeResults.Add(alternate.ToString());
                _inkOverlay.AttachedControl.Invoke(new HanziRecognizedHandler(OnThread_RecognitionWithAlternates), new object[] { sender, ev });
            }
        }

        private void inkOverlay_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {
            _strokes.Add(e.Stroke);
            _context.BackgroundRecognizeWithAlternates();
            if (_inkOverlay.DefaultDrawingAttributes.Color == FirstColor)
                _inkOverlay.DefaultDrawingAttributes.Color = SecondColor;
            else
                _inkOverlay.DefaultDrawingAttributes.Color = FirstColor;
        }

        public void Clear()
        {
            if (_inkOverlay != null)
            {
                _inkOverlay.Ink.DeleteStrokes();
                _strokes.Remove(_strokes);
                _inkOverlay.AttachedControl.Refresh();
            }
        }
    }
}

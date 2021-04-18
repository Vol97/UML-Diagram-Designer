﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UML_Diagram_Designer.FactoryClasses;
using UML_Diagram_Designer.FactoryClasses.ClassBlockFactories;
using UML_Diagram_Designer.FactoryClasses.RelationshipFactories;

namespace UML_Diagram_Designer
{
    public partial class Form1 : Form
    {
        bool _isMouseMoving = false;
        bool _isMoveButtonClicked = false;
        Point _pointForMove;
        private int _width = 6;
        private Color _color = Color.Black;
        private Canvas canvas;
        AbstractDiagramElement _currentDiagramElement;
        List<AbstractDiagramElement> listAbstractDiagramElements;
        AbstractDiagramElementFactory _currentFactory;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = Canvas.SetCanvas(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = canvas._bitmap;
            listAbstractDiagramElements = new List<AbstractDiagramElement>();
            _currentFactory = new AssociationRelationshipFactory();
        }
        private void associationButton_Click(object sender, EventArgs e)
        {
            _currentFactory= new AssociationRelationshipFactory();
            _currentDiagramElement = _currentFactory.GetElement();
        }
        private void inheritanceButton_Click(object sender, EventArgs e)
        {
            _currentFactory = new InheritanceRelationshipFactory();
            _currentDiagramElement = _currentFactory.GetElement();
        }
        private void realizationButton_Click(object sender, EventArgs e)
        {
            _currentFactory = new RealizationRelationshipFactory();
            _currentDiagramElement = _currentFactory.GetElement();
        }
        private void aggregationButton_Click(object sender, EventArgs e)
        {
            _currentFactory = new AggregationRelationshipFactory();
            _currentDiagramElement = _currentFactory.GetElement();
        }
        private void compositionButton_Click(object sender, EventArgs e)
        {
            _currentFactory = new CompositionRelationshipFactory();
            _currentDiagramElement = _currentFactory.GetElement();
        }
        private void classButton_Click(object sender, EventArgs e)
        {
            _currentFactory = new UMLClassFactory();
            _currentDiagramElement = _currentFactory.GetElement();
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            _isMouseMoving = false;
            _isMoveButtonClicked = false;
            canvas._graphics.Clear(Color.White);
            listAbstractDiagramElements.Clear();
            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _currentDiagramElement = null;
            if (_isMoveButtonClicked)
            {
                foreach (var element in listAbstractDiagramElements)
                {
                    if (element.CheckIfTheObjectIsClicked(e.Location))
                    {
                        _currentDiagramElement = element;
                        listAbstractDiagramElements.Remove(element);
                        break;
                    }
                }
                _pointForMove = e.Location;
            }
            else if (e.Button == MouseButtons.Left)
            {
                _currentDiagramElement = _currentFactory.GetElement();
                _currentDiagramElement.StartPoint = e.Location;
            }
            _isMouseMoving = true;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseMoving)
            {
                if (_isMoveButtonClicked)
                {
                    if (!(_currentDiagramElement is null))
                    {
                        _currentDiagramElement.Move(e.X - _pointForMove.X, e.Y - _pointForMove.Y);
                        _pointForMove = e.Location;
                    }
                }
                else
                {
                    _currentDiagramElement.EndPoint = e.Location;
                }
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_isMouseMoving == true)
            {
                if (!(_currentDiagramElement is null))
                {
                    canvas._graphics.Clear(pictureBox1.BackColor);
                    _currentDiagramElement.Draw(canvas);
                }
            foreach (var element in listAbstractDiagramElements)
            {
                element.Draw(canvas);
            }
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMouseMoving)
            {
                listAbstractDiagramElements.Add(_currentDiagramElement);
            }
            _isMoveButtonClicked = false;
            _isMouseMoving = false;
        }
        private void buttonMove_Click(object sender, EventArgs e)
        {
            _isMoveButtonClicked = true;
        }
        private void ColorButton_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            if (colorDialog.Color == Color.Black)
            {
                colorButton.BackColor = Color.White;
            }
            else
            {
                colorButton.BackColor = colorDialog.Color;
            }
            _color = colorDialog.Color;
        }
        private void ThicknessTrackBar_Scroll(object sender, EventArgs e)
        {
            _width = thicknessTrackBar.Value;
        }
    }
}

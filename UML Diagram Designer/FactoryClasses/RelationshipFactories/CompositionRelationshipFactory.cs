﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Diagram_Designer.ArrowParts;
using UML_Diagram_Designer.Relationships;

namespace UML_Diagram_Designer.FactoryClasses.RelationshipFactories
{
    class CompositionRelationshipFactory : AbstractDiagramElementFactory
    {
        public override AbstractDiagramElement GetElement()
        {
            return new CompositionRelationship();
        }
    }
}
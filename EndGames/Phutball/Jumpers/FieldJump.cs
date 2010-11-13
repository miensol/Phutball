using System;
using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Jumpers
{
    public class FieldJump : IJump
    {
        private readonly Tuple<int, int> _delta;
        private readonly IFieldsGraph _fieldsGraph;
        private readonly Field _from;
        private Field _endField;
        private IEnumerable<Field> _jumpedFields;

        public FieldJump(IFieldsGraph fieldsGraph, Field from, Tuple<int, int> delta)
        {
            _fieldsGraph = fieldsGraph;
            _from = from;
            _delta = delta;
        }


        public IEnumerable<Field> GetJumpedFields()
        {
            EnusreJumpsWhereSearched();
            return _jumpedFields;
        }

        public Field EndField
        {
            get
            {
                EnusreJumpsWhereSearched();
                return _endField;
            }
        }

        private void EnusreJumpsWhereSearched()
        {
            if (_jumpedFields == null)
            {
                var jumpedFieldsAndLastWhite = new FieldJumpIterator(_from, _fieldsGraph, new StoneMover(_delta))
                    .Enumerate().ToList();
                ExtractJumpedFieldsAndEndField(jumpedFieldsAndLastWhite);
            }
        }

        private void ExtractJumpedFieldsAndEndField(List<Field> jumpedFieldsAndLastWhite)
        {            
            if (jumpedFieldsAndLastWhite.Any() && jumpedFieldsAndLastWhite.Last().HasStone == false)
            {
                _endField = jumpedFieldsAndLastWhite.LastOrDefault();
                jumpedFieldsAndLastWhite.RemoveAt(jumpedFieldsAndLastWhite.Count - 1);
                _jumpedFields = jumpedFieldsAndLastWhite;
            }
            else
            {
                _jumpedFields = new List<Field>();
            }
        }
    }
}
using TagGame.Classes.Base;

namespace TagGame.Classes.Builders
{
    public class Builder
    {
        private Field _field;

        public void Build()
        {
            FieldBuild();

            FieldUpdate();
        }

        public void FieldBuild()
        {
            _field = new Field(4);
        }

        public void FieldUpdate()
        {
            _field.Change();
        }

        public Field GetField()
        {
            return _field;
        }
    }
}

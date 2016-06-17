using System.Collections.Generic;

namespace Dart_Score_Bord
{
    public class Dartboard
    {
        List<Field> fields = new List<Field>();

        public Dartboard() 
        {
            //Defineer de 62 velden van het dartbord: 20 Single, 20 Double, 20 Triple, 1 Bull, 1 Bull’s Eye.
            CreateField(FieldStatus.Single);
            CreateField(FieldStatus.Double);
            CreateField(FieldStatus.Triple);
            CreateFieldBull();
            CreateFieldBullseye();
        }

        public void CreateField(FieldStatus status) 
        {
            for (int i = 0; i < 20; i++) 
            {
                Field field = new Field() { Status = status, Value = i };
                fields.Add(field);
            }
        }
        public void CreateFieldBull() 
        {
            Field field = new Field() { Status = FieldStatus.Bull, Value = 25 };
            fields.Add(field);
        }
        public void CreateFieldBullseye()
        {
            Field field = new Field() { Status = FieldStatus.Bull, Value = 50 };
            fields.Add(field);
        }
    }
}

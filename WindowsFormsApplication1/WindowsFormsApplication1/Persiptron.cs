namespace WindowsFormsApplication1
{
    public class MyObject
    {
        public MyObject(int size)
        {
            Attributes = new int[size];
        }

        public int[] Attributes { get; set; }
    }
    public class MyFunction
    {
        public MyFunction(int size)
        {
            Attributes = new int[size];
            for (int i = 0; i < size; i++)
                Attributes[i] = 0;
        }

        public int[] Attributes { get; set; }

        public int GetValue(MyObject myObject)
        {
            int result = 0;
            for (int i = 0; i < myObject.Attributes.Length; i++)
                result += myObject.Attributes[i] * Attributes[i];

            return result;
        }
    }
    public class Persiptron
    {
        private readonly int classCount;
        private readonly int attributesCount;

        public Persiptron(int classCount, int attributesCount)
        {
            this.classCount = classCount;
            this.attributesCount = attributesCount;
        }
        private static MyFunction Sum(MyFunction func, MyObject myObject)
        {
            var result = new MyFunction(func.Attributes.Length);
            for (int i = 0; i < func.Attributes.Length; i++)
                result.Attributes[i] = func.Attributes[i] + myObject.Attributes[i];

            return result;
        }
        private static MyObject Rev(MyObject myObject)
        {
            var result = new MyObject(myObject.Attributes.Length);

            for (int i = 0; i < myObject.Attributes.Length; i++)
                result.Attributes[i] = myObject.Attributes[i] * -1;

            return result;
        }
        private MyFunction[] EmptyFunctions()
        {
            var result = new MyFunction[classCount];

            for (int i = 0; i < classCount; i++)
                result[i] = new MyFunction(attributesCount);

            return result;
        }
        public int Max(MyFunction[] result, MyObject curObject)
        {
            int max = result[0].GetValue(curObject);
            int maxClass = 0;
            int maxCount = 1;

            for (int j = 1; j < classCount; j++)
            {
                int currentValue = result[j].GetValue(curObject);
                if (currentValue > max)
                {
                    maxCount = 0;
                    max = currentValue;
                    maxClass = j;
                }
                if (currentValue == max)
                    maxCount++;
            }

            if (maxCount == 1)
                return maxClass;
            else
                return -1;
        }

        public MyFunction[] GetFuncs(MyObject[][] objects)
        {
            var result = EmptyFunctions();
            bool doNext = true;
            int i = 0, maxClass = 0;

            while (doNext && i < 1000)
            {
                for (int k = 0; k < classCount; k++)
                    for (int j = 0; j < objects[k].Length; j++)
                    {
                        maxClass = Max(result, objects[k][j]);

                        if (maxClass != k)
                        {
                            for (int l = 0; l < classCount; l++)
                                if (l == k)
                                    result[l] = Sum(result[l], objects[k][j]);
                                else
                                    result[l] = Sum(result[l], Rev(objects[k][j]));
                            doNext = true;
                        }
                    }
                i++;
            }          
            return result;
        }
    }
}

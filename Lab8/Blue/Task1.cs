namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            // Поля
            private string _name;
            protected int _votes;
            // Свойства
            public string Name => _name;
            public int Votes => _votes;
            // Конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
            // Мемтод
            public virtual int CountVotes(Response[] responses) // ПОЧЕМУ ВИРТУАЛ, что значит
            {
                int k = 0;
                for (int i = 0; i < responses.Length; i++) // подсчитываем
                {
                    if (responses[i].Name == _name)
                    {
                        k++;
                    }
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == _name)
                    {
                        responses[i]._votes = k; //обновляем его приватное поле _votes, потому что мы внутрм структуры
                    }
                }
                _votes = k;
                return _votes;
            }
            public virtual void Print()
            {
                return;
            }
        }
        public class HumanResponse : Response
        {
            // Поля
            private string _surname;
            // Свойства
            public string Surname => _surname;
            // Конструктор
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }
            // Методы
            public override int CountVotes(Response[] responses)
            {
                int k = 0;
                for (int i = 0; i < responses.Length; i++) // подсчитываем
                {
                    if (responses[i] is HumanResponse Human && Human.Name == Name && Human.Surname == _surname)//использование преобразования типов
                    {
                        k++;
                    }
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse Human && Human.Name == Name && Human.Surname == _surname)
                    {
                        Human._votes = k;
                    }
                }
                _votes = k;
                return _votes;
            }
            public override void Print()
            {
                return;
            }
        }
    }
}

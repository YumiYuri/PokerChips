using System;

namespace PokerChips
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(", "); //помещаем ввод с консоли в массив
            int[] chips = new int[input.Length]; //создаем численный массив, в который переведем значения со строкового

            int sum = 0;
            for (int i = 0; i < input.Length; i++)//преобразуем строковый массив в числовой, чтобы мы могли оперировать со значениями, как с числами
            {
                chips[i] = int.Parse(input[i]);
                sum+=chips[i];
            }
            int equality=sum/chips.Length;//поделив сумму значений на количество, получим фишки на каждого человека


            //идея алгоритма в следующем: двигаться "рукой" и отдавать/забирать фишки, уравнивая до нужного значения.  Модули того, что в руке, на каждом этапе складываем. 
            //Нюанс заключается лишь в том, что для отображения минимального количества ходов нам нужен такой массив, в котором значения либо не в большом разбросе, либо идут зеркально с обоих концов. 
            //Не придумав алгоритма для установки в такой вид, решил оставить перебор, двигая каждый раз массив циклично и смотря за результатом
            int minimum = RepeatingWaterFall();
            Swap();

            for(int i=0; i<chips.Length-1;i++)
            {
                int temporarywf = RepeatingWaterFall();
                if(minimum>temporarywf)
                    minimum = temporarywf;
                Swap();
            }

            Console.WriteLine(minimum);

            int RepeatingWaterFall()//добавляем/забираем фишки, сводя количество к искомому уровню. В это же время считаем, сколько у нас осталось на руках от шага к шагу
            {
                int waterfall = 0;
                int hand = 0;
                for (int i = 0; i < chips.Length; i++)
                {

                    hand += chips[i] - equality;
                    waterfall += Math.Abs(hand);
                }
                return waterfall;
            }

            void Swap()//просто циклический сдвиг массива
            {
                var tmp = chips[chips.Length - 1];

                for (var i = chips.Length - 1; i != 0; --i)
                {                 
                    chips[i] = chips[i - 1];                   
                }
                chips[0] = tmp;
            }
        }
    }
}



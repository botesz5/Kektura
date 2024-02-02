namespace kekturaaaaaaa;
public struct Section
{
    public string kiindulopont;
    public string vegpont;
    public double hossz;
    public int emelkedes;
    public int lejtes;
    public char pecsetelohely;

    public Section(string line)
    {
        string[] templine = line.Trim().Split(";");
        this.kiindulopont = templine[0];
        this.vegpont = templine[1];
        this.hossz = Convert.ToDouble(templine[2]);
        this.emelkedes = Convert.ToInt32(templine[3]);
        this.lejtes = Convert.ToInt32(templine[4]);
        this.pecsetelohely = Convert.ToChar(templine[5]);

    }
    // Ez itt már egy metódus (olyan függvény, amelyet a struktúrában benn készítek el.)
    // A metódus a struct-nak egy képessége. Analógia: otthon, ahol benn főzök, mosok, takarítok...
    public bool IsIncompleteName()
    {
        return !this.vegpont.Contains("pecsetelohely") && this.pecsetelohely == 'i';
    }

    public int StatusCalculate()
    {
        return this.emelkedes - this.lejtes;
    }

}
internal class Program
    {
static void Main(string[] args)
        {
        /*
        // Ez itt egy "külső" függvény volt, amit behelyztünk a struktúrába.

        bool IsIncompleteName(Section row) {
        return !row.vegpont.Contains("pecsetelohely") && row.pecsetelohely == 'i';

        }
        */

        /*
        int statusCalculate(int x; int y) 
        {
            return x - y;
        }*/

            List<Section> textDatas = new List<Section>();
            string[] textFile = File.ReadAllLines("./kektura.csv");
            for (int i = 1; i < textFile.Length; i++)
            {
                Section line = new Section(textFile[i]);
                textDatas.Add(line);

                /*line.kiindulopont = textFile[i].Trim().Split(";")[0];
                line.vegpont = textFile[i].Trim().Split(";")[1];
                line.hossz = Convert.ToDouble(textFile[i].Trim().Split(";")[2]);
                line.emelkedes = Convert.ToInt32(textFile[i].Trim().Split(";")[3]);
                line.lejtes = Convert.ToInt32(textFile[i].Trim().Split(";")[4]);
                line.pecsetelohely = Convert.ToChar(textFile[i].Trim().Split(";")[5]);
                textDatas.Add(line);*/
            }

        // A túra teljes hossza:
            double summa = 0;
            foreach (Section line in textDatas)
            {
                summa += line.hossz;
            }
            Console.WriteLine($"A túra teljes hossza: {summa:f2} km");

        // A túra legrövidebb szakaszának kiinduló pontja, hossza:
            double shortestRoute = textDatas[0].hossz;
            string startPoint = textDatas[0].kiindulopont;
            for (int i = 1; i < textDatas.Count; i++)
            {
                if (shortestRoute > textDatas[i].hossz)
                {
                    shortestRoute = textDatas[i].hossz;
                    startPoint = textDatas[i].kiindulopont;
                }
            }
            System.Console.WriteLine($"A legrövidebb szakasz kezdőpontja: {startPoint}, hossza: {shortestRoute} km");

        // Hiányos végpontok nevei:
            foreach (Section line in textDatas)
            {
                if (line.IsIncompleteName())
                {
                    Console.WriteLine($"\t{line.vegpont}");
                }
            }

            int currentHeight = 192;
            string endPoint = "";
            int maximumHeight = 0;
        foreach (Section row in textDatas)
        {
            int status = row.StatusCalculate();
            currentHeight += status;
            if (currentHeight > maximumHeight)
            {
                maximumHeight = currentHeight;
                endPoint = row.vegpont;
            }
        }
        Console.WriteLine($"Legmagasabb vegpont: {endPoint} magssaga: {maximumHeight}");
    }
}





namespace ChessBrowser;

public class ChessGame
{
    public string Event { get; set; }
    public string Site { get; set; }
    public string Round { get; set; }
    public string WhitePlayer { get; set; }
    public string BlackPlayer { get; set; }
    public int WhiteElo { get; set; }
    public int BlackElo { get; set; }
    public char Result { get; set; }
    public DateTime EventDate { get; set; }
    public string Moves { get; set; }
    
    public ChessGame(Dictionary<string, string> properties)
    {
        this.Event = properties["Event"];
        this.Site = properties["Site"];
        this.Round = properties["Round"];
        this.WhitePlayer = properties["White"];
        this.BlackPlayer = properties["Black"];
        this.WhiteElo = int.Parse(properties["WhiteElo"]);
        this.BlackElo = int.Parse(properties["BlackElo"]);
        this.Result = properties["Result"] switch
        {
            "1-0" => 'W',
            "0-1" => 'B',
            _ => 'D'
        };
        this.EventDate = DateTime.TryParse(properties["EventDate"], out var date) ? date : new DateTime();
        this.Moves = properties["Moves"];
    }
    
    public override string ToString()
    {
        return $"Event: {this.Event} \n Site: {this.Site} \n Round: {this.Round} \n White: {this.WhitePlayer} \n Black: {this.BlackPlayer} \n WhiteElo: {this.WhiteElo} \n BlackElo: {this.BlackElo} \n Result: {this.Result} \n EventDate: {this.EventDate} \n Moves: {this.Moves}";
    }
}
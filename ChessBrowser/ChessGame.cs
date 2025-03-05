namespace ChessBrowser;

public class ChessGame
{
    private string Event { get; set; }
    private string Site { get; set; }
    private string Round { get; set; }
    private string WhitePlayer { get; set; }
    private string BlackPlayer { get; set; }
    private int WhiteElo { get; set; }
    private int BlackElo { get; set; }
    private char Result { get; set; }
    private DateOnly EventDate { get; set; }
    private string Moves { get; set; }
    
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
        this.EventDate = DateOnly.TryParse(properties["EventDate"], out var date) ? date : new DateOnly();
        this.Moves = properties["Moves"];
    }
    
    public override string ToString()
    {
        return $"Event: {this.Event} \n Site: {this.Site} \n Round: {this.Round} \n White: {this.WhitePlayer} \n Black: {this.BlackPlayer} \n WhiteElo: {this.WhiteElo} \n BlackElo: {this.BlackElo} \n Result: {this.Result} \n EventDate: {this.EventDate} \n Moves: {this.Moves}";
    }
}
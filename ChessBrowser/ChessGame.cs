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
    private List<String> Moves { get; set; }
}
using System;
using ChessBrowser;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessBrowser.Tests;

[TestClass]
[TestSubject(typeof(PgnParser))]
public class PgnParserTest
{

    [TestMethod]
    public void METHOD()
    {
        string pgn =
            "[Event \"ETCC\"]\r\n[Site \"Crete GRE\"]\r\n[Date \"2007.10.30\"]\r\n[Round \"3\"]\r\n[White \"Cheparinov, Ivan\"]\r\n[Black \"Drasko, Milan\"]\r\n[Result \"1-0\"]\r\n[WhiteElo \"2670\"]\r\n[BlackElo \"2557\"]\r\n[ECO \"A17\"]\r\n[EventDate \"2007.10.28\"]\r\n\r\n1.Nf3 Nf6 2.c4 e6 3.Nc3 Bb4 4.Qc2 O-O 5.a3 Bxc3 6.Qxc3 d6 7.e3 a5 8.b3 Re8\r\n9.d4 Nc6 10.Bb2 e5 11.d5 Ne7 12.Nd2 b5 13.e4 bxc4 14.Bxc4 Bd7 15.O-O c6 \r\n16.dxc6 Bxc6 17.f4 Qb6+ 18.Kh1 Ng6 19.fxe5 dxe5 20.a4 Rad8 21.Rac1 Nf4 22.\r\nBxf7+ Kxf7 23.Nc4 Qd4 24.Nxe5+ Rxe5 25.Qxd4 Rxd4 26.Bxd4 Rxe4 27.Bxf6 1-0\r\n\r\n[Event \"7. Rohde Open\"]\r\n[Site \"Sautron FRA\"]\r\n[Date \"2007.10.30\"]\r\n[Round \"5\"]\r\n[White \"Malakhatko, Vadim\"]\r\n[Black \"Zozulia, A.\"]\r\n[Result \"1-0\"]\r\n[WhiteElo \"2603\"]\r\n[BlackElo \"2347\"]\r\n[ECO \"A35\"]\r\n[EventDate \"2007.10.27\"]\r\n\r\n1.c4 c5 2.Nf3 Nc6 3.Nc3 e5 4.e3 d6 5.Be2 Nge7 6.O-O Nf5 7.a3 g6 8.Rb1 Bg7 \r\n9.b4 O-O 10.d3 b6 11.Nd2 Bb7 12.Bf3 Rb8 13.g3 h5 14.Bg2 Qd7 15.Qa4 Nfe7 \r\n16.Nd5 Rfd8 17.bxc5 dxc5 18.Bb2 Na5 19.Qxd7 Rxd7 20.Bh3 f5 21.e4 Rf8 22.\r\nNf3 Nac6 23.Ng5 Rd6 24.f4 exf4 25.Bxg7 Kxg7 26.gxf4 Nxd5 27.exd5 Nd4 28.\r\nRfe1 Rd7 29.a4 Kf6 30.Kf2 Rfd8 31.Ne6 Nxe6 32.Rxe6+ Kf7 33.a5 bxa5 34.Rb5 \r\nRc7 35.Rxa5 Bc8 36.Re2 Rd6 37.Rea2 a6 38.Rb2 Kf6 39.Bg2 g5 40.d4 cxd4 41.\r\nc5 Rd8 42.Rb6+ Kg7 43.d6 Ra7 44.Rb4 gxf4 45.Rxd4 Kf6 46.c6 Be6 47.Rc5 Rg7 \r\n48.Bf1 Bc8 49.d7 Bxd7 50.Rcd5 1-0\r\n\n";
        string[] fileLines = pgn.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        PgnParser.pgnReader(fileLines);
    }
}
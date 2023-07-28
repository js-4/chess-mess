using ChessChallenge.API;
using System;
using System.Collections.Generic;

public class MyBot : IChessBot
{
    // Piece values: null, bauer, pferd, läufer, turm, dame, könig
    int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };

    public Move Think(Board board, Timer timer)
    {
        Move[] allMoves = board.GetLegalMoves();

        // Pick a random move to play if nothing better is found
        Random rng = new();
        Move moveToPlay = allMoves[rng.Next(allMoves.Length)];
        int highestValueCapture = 0;

        foreach (Move move in allMoves)
        {
            // Always play checkmate in one
            if (MoveIsCheckmate(board, move))
            {
                moveToPlay = move;
                break;
            }

            List<Move> moves = MoveCanBeCapturedFrom(board, move);
            Debug(board, moves);


        }

        return allMoves[0];
    }

    // Test if this move gives checkmate
    bool MoveIsCheckmate(Board board, Move move)
    {
        board.MakeMove(move);
        bool isMate = board.IsInCheckmate();
        board.UndoMove(move);
        return isMate;
    }

    List<Move> MoveCanBeCapturedFrom(Board board, Move currentMove)
    {
        board.MakeMove(currentMove);
        Move[] opponentMoves = board.GetLegalMoves(true);
        List<Move> allMoves = new List<Move>();

        foreach (Move move in opponentMoves)
        {
            if (currentMove.TargetSquare.Equals(move.TargetSquare))
            {
                allMoves.Add(move);
            }
        }

        board.UndoMove(currentMove);
        return allMoves;
    }

    void Debug(Board board, List<Move> list)
    {

        foreach (Move obj in list)
        {
            string color = !board.IsWhiteToMove ? "White:" : "Black:";
            Console.WriteLine($"{color} {obj.ToString()}");
        }
    }
}


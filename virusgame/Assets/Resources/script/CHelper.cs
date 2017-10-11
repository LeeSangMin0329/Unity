using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CHelper {

    public static List<short> find_available_cells(short basis_cell, List<short> total_cells, List<CPlayer> players)
    {
        List<short> targets = find_neighbor_cells(basis_cell, total_cells, 2);

        players.ForEach(obj =>
        {
            targets.RemoveAll(number => obj.cell_indexes.Exists(cell => cell == number));
        });

        return targets;
    }

    public static List<short> find_neighbor_cells(short basis_cell, List<short> targets, short gap)
    {
        Vector2 pos = convert_to_position(basis_cell);
        return targets.FindAll(obj => get_distance(pos, convert_to_position(obj)) <= gap);
    }

    // overwrite ---------------
    public static byte get_distance(short from, short to)
    {
        return get_distance(convert_to_position(from), convert_to_position(to));
    }

    public static byte get_distance(Vector2 pos1, Vector2 pos2)
    {
        Vector2 distance = (pos1 - pos2);
        short x = (short)Mathf.Abs(distance.x);
        short y = (short)Mathf.Abs(distance.y);

        return (byte)Mathf.Max(x, y);
    }
    //-----------------------


    public static short calc_row(short cell)
    {
        return (short)(cell / CBattleRoom.COL_COUNT);
    }

    public static short calc_col(short cell)
    {
        return (short)(cell % CBattleRoom.COL_COUNT);
    }

    public static Vector2 convert_to_position(short cell)
    {
        return new Vector2(calc_row(cell), calc_col(cell));
    }

    public static byte howfar_from_clicked_cell(short basis_cell, short cell)
    {
        short row = (short)(basis_cell / CBattleRoom.COL_COUNT);
        short col = (short)(basis_cell % CBattleRoom.COL_COUNT);
        Vector2 basis_pos = new Vector2(col, row);

        row = (short)(cell / CBattleRoom.COL_COUNT);
        col = (short)(cell % CBattleRoom.COL_COUNT);
        Vector2 cell_pos = new Vector2(col, row);

        Vector2 distance = (basis_pos - cell_pos);
        short x = (short)Mathf.Abs(distance.x);
        short y = (short)Mathf.Abs(distance.y);

        return (byte)Mathf.Max(x, y);
        
    }

    public static bool can_play_more(List<short> board, List<CPlayer> players, int current_player_index)
    {
        CPlayer current = players[current_player_index];
        foreach (byte cell in current.cell_indexes)
        {
            if (CHelper.find_available_cells(cell, board, players).Count > 0)
                return true;
        }
        return false;
    }

}

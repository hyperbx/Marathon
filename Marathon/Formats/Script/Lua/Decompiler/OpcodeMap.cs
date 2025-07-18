using Marathon.Formats.Script.Lua.Version;

namespace Marathon.Formats.Script.Lua.Decompiler
{
	public class OpcodeMap
	{
		private readonly Opcode[] _map;

		public OpcodeMap(LuaVersion in_versionNumber)
		{
			if (in_versionNumber == LuaVersion.Lua50)
			{
				_map = new Opcode[35];
				_map[0] = Opcode.MOVE;
				_map[1] = Opcode.LOADK;
				_map[2] = Opcode.LOADBOOL;
				_map[3] = Opcode.LOADNIL;
				_map[4] = Opcode.GETUPVAL;
				_map[5] = Opcode.GETGLOBAL;
				_map[6] = Opcode.GETTABLE;
				_map[7] = Opcode.SETGLOBAL;
				_map[8] = Opcode.SETUPVAL;
				_map[9] = Opcode.SETTABLE;
				_map[10] = Opcode.NEWTABLE;
				_map[11] = Opcode.SELF;
				_map[12] = Opcode.ADD;
				_map[13] = Opcode.SUB;
				_map[14] = Opcode.MUL;
				_map[15] = Opcode.DIV;
				_map[16] = Opcode.POW;
				_map[17] = Opcode.UNM;
				_map[18] = Opcode.NOT;
				_map[19] = Opcode.CONCAT;
				_map[20] = Opcode.JMP;
				_map[21] = Opcode.EQ;
				_map[22] = Opcode.LT;
				_map[23] = Opcode.LE;
				_map[24] = Opcode.TEST50;
				_map[25] = Opcode.CALL;
				_map[26] = Opcode.TAILCALL;
				_map[27] = Opcode.RETURN;
				_map[28] = Opcode.FORLOOP;
				_map[29] = Opcode.TFORLOOP;
				_map[30] = Opcode.TFORPREP;
				_map[31] = Opcode.SETLIST50;
				_map[32] = Opcode.SETLISTO;
				_map[33] = Opcode.CLOSE;
				_map[34] = Opcode.CLOSURE;
			}
			else if (in_versionNumber == LuaVersion.Lua51)
			{
				_map = new Opcode[38];
				_map[0] = Opcode.MOVE;
				_map[1] = Opcode.LOADK;
				_map[2] = Opcode.LOADBOOL;
				_map[3] = Opcode.LOADNIL;
				_map[4] = Opcode.GETUPVAL;
				_map[5] = Opcode.GETGLOBAL;
				_map[6] = Opcode.GETTABLE;
				_map[7] = Opcode.SETGLOBAL;
				_map[8] = Opcode.SETUPVAL;
				_map[9] = Opcode.SETTABLE;
				_map[10] = Opcode.NEWTABLE;
				_map[11] = Opcode.SELF;
				_map[12] = Opcode.ADD;
				_map[13] = Opcode.SUB;
				_map[14] = Opcode.MUL;
				_map[15] = Opcode.DIV;
				_map[16] = Opcode.MOD;
				_map[17] = Opcode.POW;
				_map[18] = Opcode.UNM;
				_map[19] = Opcode.NOT;
				_map[20] = Opcode.LEN;
				_map[21] = Opcode.CONCAT;
				_map[22] = Opcode.JMP;
				_map[23] = Opcode.EQ;
				_map[24] = Opcode.LT;
				_map[25] = Opcode.LE;
				_map[26] = Opcode.TEST;
				_map[27] = Opcode.TESTSET;
				_map[28] = Opcode.CALL;
				_map[29] = Opcode.TAILCALL;
				_map[30] = Opcode.RETURN;
				_map[31] = Opcode.FORLOOP;
				_map[32] = Opcode.FORPREP;
				_map[33] = Opcode.TFORLOOP;
				_map[34] = Opcode.SETLIST;
				_map[35] = Opcode.CLOSE;
				_map[36] = Opcode.CLOSURE;
				_map[37] = Opcode.VARARG;
			}
			else
			{
				_map = new Opcode[40];
				_map[0] = Opcode.MOVE;
				_map[1] = Opcode.LOADK;
				_map[2] = Opcode.LOADKX;
				_map[3] = Opcode.LOADBOOL;
				_map[4] = Opcode.LOADNIL;
				_map[5] = Opcode.GETUPVAL;
				_map[6] = Opcode.GETTABUP;
				_map[7] = Opcode.GETTABLE;
				_map[8] = Opcode.SETTABUP;
				_map[9] = Opcode.SETUPVAL;
				_map[10] = Opcode.SETTABLE;
				_map[11] = Opcode.NEWTABLE;
				_map[12] = Opcode.SELF;
				_map[13] = Opcode.ADD;
				_map[14] = Opcode.SUB;
				_map[15] = Opcode.MUL;
				_map[16] = Opcode.DIV;
				_map[17] = Opcode.MOD;
				_map[18] = Opcode.POW;
				_map[19] = Opcode.UNM;
				_map[20] = Opcode.NOT;
				_map[21] = Opcode.LEN;
				_map[22] = Opcode.CONCAT;
				_map[23] = Opcode.JMP;
				_map[24] = Opcode.EQ;
				_map[25] = Opcode.LT;
				_map[26] = Opcode.LE;
				_map[27] = Opcode.TEST;
				_map[28] = Opcode.TESTSET;
				_map[29] = Opcode.CALL;
				_map[30] = Opcode.TAILCALL;
				_map[31] = Opcode.RETURN;
				_map[32] = Opcode.FORLOOP;
				_map[33] = Opcode.FORPREP;
				_map[34] = Opcode.TFORCALL;
				_map[35] = Opcode.TFORLOOP;
				_map[36] = Opcode.SETLIST;
				_map[37] = Opcode.CLOSURE;
				_map[38] = Opcode.VARARG;
				_map[39] = Opcode.EXTRAARG;
			}
		}

		public virtual Opcode Get(int in_opcode)
		{
			if (in_opcode >= 0 && in_opcode < _map.Length)
			{
				return _map[in_opcode];
			}
			else
			{
				return Opcode.NULL;
			}
		}
	}
}

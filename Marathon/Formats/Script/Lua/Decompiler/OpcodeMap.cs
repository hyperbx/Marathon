namespace Marathon.Formats.Script.Lua.Decompiler
{
	public class OpcodeMap
	{
		private Op.Opcode[] _map;

		public OpcodeMap(int version)
		{
			if (version == 0x50)
			{
				_map = new Op.Opcode[35];
				_map[0] = Op.Opcode.MOVE;
				_map[1] = Op.Opcode.LOADK;
				_map[2] = Op.Opcode.LOADBOOL;
				_map[3] = Op.Opcode.LOADNIL;
				_map[4] = Op.Opcode.GETUPVAL;
				_map[5] = Op.Opcode.GETGLOBAL;
				_map[6] = Op.Opcode.GETTABLE;
				_map[7] = Op.Opcode.SETGLOBAL;
				_map[8] = Op.Opcode.SETUPVAL;
				_map[9] = Op.Opcode.SETTABLE;
				_map[10] = Op.Opcode.NEWTABLE;
				_map[11] = Op.Opcode.SELF;
				_map[12] = Op.Opcode.ADD;
				_map[13] = Op.Opcode.SUB;
				_map[14] = Op.Opcode.MUL;
				_map[15] = Op.Opcode.DIV;
				_map[16] = Op.Opcode.POW;
				_map[17] = Op.Opcode.UNM;
				_map[18] = Op.Opcode.NOT;
				_map[19] = Op.Opcode.CONCAT;
				_map[20] = Op.Opcode.JMP;
				_map[21] = Op.Opcode.EQ;
				_map[22] = Op.Opcode.LT;
				_map[23] = Op.Opcode.LE;
				_map[24] = Op.Opcode.TEST50;
				_map[25] = Op.Opcode.CALL;
				_map[26] = Op.Opcode.TAILCALL;
				_map[27] = Op.Opcode.RETURN;
				_map[28] = Op.Opcode.FORLOOP;
				_map[29] = Op.Opcode.TFORLOOP;
				_map[30] = Op.Opcode.TFORPREP;
				_map[31] = Op.Opcode.SETLIST50;
				_map[32] = Op.Opcode.SETLISTO;
				_map[33] = Op.Opcode.CLOSE;
				_map[34] = Op.Opcode.CLOSURE;
			}
			else if (version == 0x51)
			{
				_map = new Op.Opcode[38];
				_map[0] = Op.Opcode.MOVE;
				_map[1] = Op.Opcode.LOADK;
				_map[2] = Op.Opcode.LOADBOOL;
				_map[3] = Op.Opcode.LOADNIL;
				_map[4] = Op.Opcode.GETUPVAL;
				_map[5] = Op.Opcode.GETGLOBAL;
				_map[6] = Op.Opcode.GETTABLE;
				_map[7] = Op.Opcode.SETGLOBAL;
				_map[8] = Op.Opcode.SETUPVAL;
				_map[9] = Op.Opcode.SETTABLE;
				_map[10] = Op.Opcode.NEWTABLE;
				_map[11] = Op.Opcode.SELF;
				_map[12] = Op.Opcode.ADD;
				_map[13] = Op.Opcode.SUB;
				_map[14] = Op.Opcode.MUL;
				_map[15] = Op.Opcode.DIV;
				_map[16] = Op.Opcode.MOD;
				_map[17] = Op.Opcode.POW;
				_map[18] = Op.Opcode.UNM;
				_map[19] = Op.Opcode.NOT;
				_map[20] = Op.Opcode.LEN;
				_map[21] = Op.Opcode.CONCAT;
				_map[22] = Op.Opcode.JMP;
				_map[23] = Op.Opcode.EQ;
				_map[24] = Op.Opcode.LT;
				_map[25] = Op.Opcode.LE;
				_map[26] = Op.Opcode.TEST;
				_map[27] = Op.Opcode.TESTSET;
				_map[28] = Op.Opcode.CALL;
				_map[29] = Op.Opcode.TAILCALL;
				_map[30] = Op.Opcode.RETURN;
				_map[31] = Op.Opcode.FORLOOP;
				_map[32] = Op.Opcode.FORPREP;
				_map[33] = Op.Opcode.TFORLOOP;
				_map[34] = Op.Opcode.SETLIST;
				_map[35] = Op.Opcode.CLOSE;
				_map[36] = Op.Opcode.CLOSURE;
				_map[37] = Op.Opcode.VARARG;
			}
			else
			{
				_map = new Op.Opcode[40];
				_map[0] = Op.Opcode.MOVE;
				_map[1] = Op.Opcode.LOADK;
				_map[2] = Op.Opcode.LOADKX;
				_map[3] = Op.Opcode.LOADBOOL;
				_map[4] = Op.Opcode.LOADNIL;
				_map[5] = Op.Opcode.GETUPVAL;
				_map[6] = Op.Opcode.GETTABUP;
				_map[7] = Op.Opcode.GETTABLE;
				_map[8] = Op.Opcode.SETTABUP;
				_map[9] = Op.Opcode.SETUPVAL;
				_map[10] = Op.Opcode.SETTABLE;
				_map[11] = Op.Opcode.NEWTABLE;
				_map[12] = Op.Opcode.SELF;
				_map[13] = Op.Opcode.ADD;
				_map[14] = Op.Opcode.SUB;
				_map[15] = Op.Opcode.MUL;
				_map[16] = Op.Opcode.DIV;
				_map[17] = Op.Opcode.MOD;
				_map[18] = Op.Opcode.POW;
				_map[19] = Op.Opcode.UNM;
				_map[20] = Op.Opcode.NOT;
				_map[21] = Op.Opcode.LEN;
				_map[22] = Op.Opcode.CONCAT;
				_map[23] = Op.Opcode.JMP;
				_map[24] = Op.Opcode.EQ;
				_map[25] = Op.Opcode.LT;
				_map[26] = Op.Opcode.LE;
				_map[27] = Op.Opcode.TEST;
				_map[28] = Op.Opcode.TESTSET;
				_map[29] = Op.Opcode.CALL;
				_map[30] = Op.Opcode.TAILCALL;
				_map[31] = Op.Opcode.RETURN;
				_map[32] = Op.Opcode.FORLOOP;
				_map[33] = Op.Opcode.FORPREP;
				_map[34] = Op.Opcode.TFORCALL;
				_map[35] = Op.Opcode.TFORLOOP;
				_map[36] = Op.Opcode.SETLIST;
				_map[37] = Op.Opcode.CLOSURE;
				_map[38] = Op.Opcode.VARARG;
				_map[39] = Op.Opcode.EXTRAARG;
			}
		}

		public virtual Op.Opcode Get(int opNumber)
		{
			if (opNumber >= 0 && opNumber < _map.Length)
			{
				return _map[opNumber];
			}
			else
			{
				return Op.Opcode.NULL;
			}
		}
	}
}

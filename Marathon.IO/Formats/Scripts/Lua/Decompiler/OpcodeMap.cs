namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
	public class OpcodeMap
	{
		private Op.Opcode[] map;

		public OpcodeMap(int version)
		{
			if (version == 0x50)
			{
				map = new Op.Opcode[35];
				map[0] = Op.Opcode.MOVE;
				map[1] = Op.Opcode.LOADK;
				map[2] = Op.Opcode.LOADBOOL;
				map[3] = Op.Opcode.LOADNIL;
				map[4] = Op.Opcode.GETUPVAL;
				map[5] = Op.Opcode.GETGLOBAL;
				map[6] = Op.Opcode.GETTABLE;
				map[7] = Op.Opcode.SETGLOBAL;
				map[8] = Op.Opcode.SETUPVAL;
				map[9] = Op.Opcode.SETTABLE;
				map[10] = Op.Opcode.NEWTABLE;
				map[11] = Op.Opcode.SELF;
				map[12] = Op.Opcode.ADD;
				map[13] = Op.Opcode.SUB;
				map[14] = Op.Opcode.MUL;
				map[15] = Op.Opcode.DIV;
				map[16] = Op.Opcode.POW;
				map[17] = Op.Opcode.UNM;
				map[18] = Op.Opcode.NOT;
				map[19] = Op.Opcode.CONCAT;
				map[20] = Op.Opcode.JMP;
				map[21] = Op.Opcode.EQ;
				map[22] = Op.Opcode.LT;
				map[23] = Op.Opcode.LE;
				map[24] = Op.Opcode.TEST50;
				map[25] = Op.Opcode.CALL;
				map[26] = Op.Opcode.TAILCALL;
				map[27] = Op.Opcode.RETURN;
				map[28] = Op.Opcode.FORLOOP;
				map[29] = Op.Opcode.TFORLOOP;
				map[30] = Op.Opcode.TFORPREP;
				map[31] = Op.Opcode.SETLIST50;
				map[32] = Op.Opcode.SETLISTO;
				map[33] = Op.Opcode.CLOSE;
				map[34] = Op.Opcode.CLOSURE;
			}
			else if (version == 0x51)
			{
				map = new Op.Opcode[38];
				map[0] = Op.Opcode.MOVE;
				map[1] = Op.Opcode.LOADK;
				map[2] = Op.Opcode.LOADBOOL;
				map[3] = Op.Opcode.LOADNIL;
				map[4] = Op.Opcode.GETUPVAL;
				map[5] = Op.Opcode.GETGLOBAL;
				map[6] = Op.Opcode.GETTABLE;
				map[7] = Op.Opcode.SETGLOBAL;
				map[8] = Op.Opcode.SETUPVAL;
				map[9] = Op.Opcode.SETTABLE;
				map[10] = Op.Opcode.NEWTABLE;
				map[11] = Op.Opcode.SELF;
				map[12] = Op.Opcode.ADD;
				map[13] = Op.Opcode.SUB;
				map[14] = Op.Opcode.MUL;
				map[15] = Op.Opcode.DIV;
				map[16] = Op.Opcode.MOD;
				map[17] = Op.Opcode.POW;
				map[18] = Op.Opcode.UNM;
				map[19] = Op.Opcode.NOT;
				map[20] = Op.Opcode.LEN;
				map[21] = Op.Opcode.CONCAT;
				map[22] = Op.Opcode.JMP;
				map[23] = Op.Opcode.EQ;
				map[24] = Op.Opcode.LT;
				map[25] = Op.Opcode.LE;
				map[26] = Op.Opcode.TEST;
				map[27] = Op.Opcode.TESTSET;
				map[28] = Op.Opcode.CALL;
				map[29] = Op.Opcode.TAILCALL;
				map[30] = Op.Opcode.RETURN;
				map[31] = Op.Opcode.FORLOOP;
				map[32] = Op.Opcode.FORPREP;
				map[33] = Op.Opcode.TFORLOOP;
				map[34] = Op.Opcode.SETLIST;
				map[35] = Op.Opcode.CLOSE;
				map[36] = Op.Opcode.CLOSURE;
				map[37] = Op.Opcode.VARARG;
			}
			else
			{
				map = new Op.Opcode[40];
				map[0] = Op.Opcode.MOVE;
				map[1] = Op.Opcode.LOADK;
				map[2] = Op.Opcode.LOADKX;
				map[3] = Op.Opcode.LOADBOOL;
				map[4] = Op.Opcode.LOADNIL;
				map[5] = Op.Opcode.GETUPVAL;
				map[6] = Op.Opcode.GETTABUP;
				map[7] = Op.Opcode.GETTABLE;
				map[8] = Op.Opcode.SETTABUP;
				map[9] = Op.Opcode.SETUPVAL;
				map[10] = Op.Opcode.SETTABLE;
				map[11] = Op.Opcode.NEWTABLE;
				map[12] = Op.Opcode.SELF;
				map[13] = Op.Opcode.ADD;
				map[14] = Op.Opcode.SUB;
				map[15] = Op.Opcode.MUL;
				map[16] = Op.Opcode.DIV;
				map[17] = Op.Opcode.MOD;
				map[18] = Op.Opcode.POW;
				map[19] = Op.Opcode.UNM;
				map[20] = Op.Opcode.NOT;
				map[21] = Op.Opcode.LEN;
				map[22] = Op.Opcode.CONCAT;
				map[23] = Op.Opcode.JMP;
				map[24] = Op.Opcode.EQ;
				map[25] = Op.Opcode.LT;
				map[26] = Op.Opcode.LE;
				map[27] = Op.Opcode.TEST;
				map[28] = Op.Opcode.TESTSET;
				map[29] = Op.Opcode.CALL;
				map[30] = Op.Opcode.TAILCALL;
				map[31] = Op.Opcode.RETURN;
				map[32] = Op.Opcode.FORLOOP;
				map[33] = Op.Opcode.FORPREP;
				map[34] = Op.Opcode.TFORCALL;
				map[35] = Op.Opcode.TFORLOOP;
				map[36] = Op.Opcode.SETLIST;
				map[37] = Op.Opcode.CLOSURE;
				map[38] = Op.Opcode.VARARG;
				map[39] = Op.Opcode.EXTRAARG;
			}
		}

		public virtual Op.Opcode get(int opNumber)
		{
			if (opNumber >= 0 && opNumber < map.Length)
				return map[opNumber];

			else
				return Op.Opcode.NULL;
		}
	}
}

using System.Collections.Generic;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
	public sealed class Op
	{
		public enum Opcode
		{
			MOVE,
			LOADK,
			LOADBOOL,
			LOADNIL,
			GETUPVAL,
			GETGLOBAL,
			GETTABLE,
			SETGLOBAL,
			SETUPVAL,
			SETTABLE,
			NEWTABLE,
			SELF,
			ADD,
			SUB,
			MUL,
			DIV,
			MOD,
			POW,
			UNM,
			NOT,
			LEN,
			CONCAT,
			JMP,
			EQ,
			LT,
			LE,
			TEST,
			TESTSET,
			CALL,
			TAILCALL,
			RETURN,
			FORLOOP,
			FORPREP,
			TFORLOOP,
			SETLIST,
			CLOSE,
			CLOSURE,
			VARARG,
			LOADKX,
			GETTABUP,
			SETTABUP,
			TFORCALL,
			EXTRAARG,
			SETLIST50,
			SETLISTO,
			TFORPREP,
			TEST50,
			NULL
		}

		private readonly OpcodeFormat format;

		private Op(OpcodeFormat format) => this.format = format;

		//public string codePointToString(int codepoint, ICodeExtract ex)
		//{
		//	switch (format)
		//	{
		//		case OpcodeFormat.A:
		//			return $"{ToString()} {ex.extract_A(codepoint)}";

		//		case OpcodeFormat.A_B:
		//			return $"{ToString()} {ex.extract_A(codepoint)} {ex.extract_B(codepoint)}";

		//		case OpcodeFormat.A_C:
		//			return $"{ToString()} {ex.extract_A(codepoint)} {ex.extract_C(codepoint)}";

		//		case OpcodeFormat.A_B_C:
		//			return $"{ToString()} {ex.extract_A(codepoint)} {ex.extract_B(codepoint)} {ex.extract_C(codepoint)}";

		//		case OpcodeFormat.A_Bx:
		//			return $"{ToString()} {ex.extract_A(codepoint)} {ex.extract_Bx(codepoint)}";

		//		case OpcodeFormat.A_sBx:
		//			return $"{ToString()} {ex.extract_A(codepoint)} {ex.extract_sBx(codepoint)}";

		//		case OpcodeFormat.Ax:
		//			return $"{ToString()} <Ax>";

		//		case OpcodeFormat.sBx:
		//			return $"{ToString()} {ex.extract_sBx(codepoint)}";

		//		default:
		//			return ToString();
		//	}
		//}
	}

}

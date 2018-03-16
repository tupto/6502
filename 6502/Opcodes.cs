using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6502
{
    public static class Opcodes
    {
        //ADC (Add with carry)
        //  Flags: N, V, Z, C
        public const byte ADC_IMMEDIATE = 0x69;     //2 cycles
        public const byte ADC_ZEROPAGE = 0x65;      //3 cycles
        public const byte ADC_ZEROPAGE_X = 0x75;    //4 cycles
        public const byte ADC_ABSOLUTE = 0x6D;      //4 cycles
        public const byte ADC_ABSOLUTE_X = 0x7D;    //4 cycles (+1 if page boundary crossed)
        public const byte ADC_ABSOLUTE_Y = 0x79;    //4 cycles (+1 if page boundary crossed)
        public const byte ADC_INDIRECT_X = 0x61;    //6 cycles
        public const byte ADC_INDIRECT_Y = 0x71;    //5 cycles (+1 if page boundary crossed)

        //AND (bitwise AND with accumulator)
        //  Flags: N, Z
        public const byte AND_IMMEDIATE = 0x29;     //2 cycles
        public const byte AND_ZEROPAGE = 0x25;      //3 cycles
        public const byte AND_ZEROPAGE_X = 0x35;    //4 cycles
        public const byte AND_ABSOLUTE = 0x2D;      //4 cycles
        public const byte AND_ABSOLUTE_X = 0x3D;    //4 cycles (+1 if page boundary crossed)
        public const byte AND_ABSOLUTE_Y = 0x39;    //4 cycles (+1 if page boundary crossed)
        public const byte AND_INDIRECT_X = 0x21;    //6 cycles
        public const byte AND_INDIRECT_Y = 0x31;    //5 cycles (+1 if page boundary crossed)

        //ASL (arithmetic shift left)
        //  Flags: N, Z, C
        public const byte ASL_ACCUMULATOR = 0x0A;   //2 cycles
        public const byte ASL_ZEROPAGE = 0x06;      //5 cycles
        public const byte ASL_ZEROPAGE_X = 0x16;    //6 cycles
        public const byte ASL_ABSOLUTE = 0x0E;      //6 cycles
        public const byte ASL_ABSOLUTE_X = 0x1E;    //7 cycles

        //BCC (branch on C = 0)
        public const byte BCC = 0x90;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //BCS (branch on C = 1)
        public const byte BCS = 0xB0;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //BEQ (branch on Z = 1)
        public const byte BEQ = 0xF0;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //BIT (test bits in memory with accumulator)
        //  Flags: N, Z, V
        public const byte BIT_ZEROPAGE = 0x24;      //3 cycles
        public const byte BIT_ABSOLUTE = 0x2C;      //4 cycles

        //BMI (branch  on N = 1)
        public const byte BMI = 0x30;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //BNE (branch on Z = 0)
        public const byte BNE = 0xD0;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //BPL (branch on N = 0)
        public const byte BPL = 0x10;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //BRK (force break)
        //  Flags: I = 1
        public const byte BRK = 0x00;               //7 cycles

        //BVC (branch on V = 0)
        public const byte BVC = 0x50;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //BVS (branch on V = 1)
        public const byte BVS = 0x70;               //2 cycles (+1 if branch is on the same page, +2 if branch is on a different page)

        //CLC (clear carry flag)
        //  Flags: C = 0
        public const byte CLC = 0x18;               //2 cycles

        //CLD (clear decimal mode)
        //  Flags: D = 0
        public const byte CLD = 0xD8;               //2 cycles

        //CLI (clear interrupt disable bit)
        //  Flags: I = 0
        public const byte CLI = 0x58;               //2 cycles

        //CLV (clear overflow flag)
        //  Flags: V = 0
        public const byte CLV = 0xB8;               //2 cycles

        //CMP (compare memory with accumulator)
        //  Flags: N, Z, C
        public const byte CMP_IMMEDIATE = 0xC9;     //2 cycles
        public const byte CMP_ZEROPAGE = 0xC5;      //3 cycles
        public const byte CMP_ZEROPAGE_X = 0xD5;    //4 cycles
        public const byte CMP_ABSOLUTE = 0xCD;      //4 cycles
        public const byte CMP_ABSOLUTE_X = 0xDD;    //4 cycles (+1 if page boundary crossed)
        public const byte CMP_ABSOLUTE_Y = 0xD9;    //4 cycles (+1 if page boundary crossed)
        public const byte CMP_INDIRECT_X = 0xC1;    //6 cycles
        public const byte CMP_INDIRECT_Y = 0xD1;    //5 cycles (+1 if page boundary crossed)

        //CPX (compare memory with index X)
        //  Flags: N, Z, C
        public const byte CPX_IMMEDIATE = 0xE0;     //2 cycles
        public const byte CPX_ZEROPAGE = 0xE4;      //3 cycles
        public const byte CPX_ABSOLUTE = 0xEC;      //4 cycles

        //CPY (compare memory with index Y)
        //  Flags: N, Z, C
        public const byte CPY_IMMEDIATE = 0xC0;     //2 cycles
        public const byte CPY_ZEROPAGE = 0xC4;      //3 cycles
        public const byte CPY_ABSOLUTE = 0xCC;      //4 cycles

        //DEC (decrement memory by one)
        //  Flags: N, Z
        public const byte DEC_ZEROPAGE = 0xC6;      //5 cycles
        public const byte DEC_ZEROPAGE_X = 0xD6;    //6 cycles
        public const byte DEC_ABSOLUTE = 0xCE;      //3 cycles
        public const byte DEC_ABSOLUTE_X = 0xDE;    //7 cycles

        //DEX (decrement index X by one)
        //  Flags: N, Z
        public const byte DEX = 0xCA;               //2 cycles

        //DEY (decrement index Y by one)
        // Flags: N, Z
        public const byte DEY = 0x88;               //2 cycles

        //EOR (XOR memory with accumulator)
        //  Flags N, Z
        public const byte EOR_IMMEDIATE = 0x49;     //2 cycles
        public const byte EOR_ZEROPAGE = 0x45;      //3 cycles
        public const byte EOR_ZEROPAGE_X = 0x55;    //4 cycles
        public const byte EOR_ABSOLUTE = 0x4D;      //4 cycles
        public const byte EOR_ABSOLUTE_X = 0x5D;    //4 cycles (+1 if page boundary crossed)
        public const byte EOR_ABSOLUTE_Y = 0x59;    //4 cycles (+1 if page boundary crossed)
        public const byte EOR_INDIRECT_X = 0x41;    //6 cycles
        public const byte EOR_INDIRECT_Y = 0x51;    //5 cycles (+1 if page boundary crossed)

        //INC (increment memory by one)
        //  Flags: N, Z
        public const byte INC_ZEROPAGE = 0xE6;      //5 cycles
        public const byte INC_ZEROPAGE_X = 0xF6;    //6 cycles
        public const byte INC_ABSOLUTE = 0xEE;      //6 cycles
        public const byte INC_ABSOLUTE_X = 0xFE;    //7 cycles

        //INX (increment index X by one)
        //  Flags: N, Z
        public const byte INX = 0xE8;               //2 cycles

        //INY (increment index Y by one)
        //  Flags: N, Z
        public const byte INY = 0xC8;               //2 cycles

        //JMP (jump to a new location)
        public const byte JMP_ABSOLUTE = 0x4C;      //3 cycles
        public const byte JMP_INDIRECT = 0x6C;      //5 cycles

        //JSR (jump to a new location and save the return address)
        public const byte JSR = 0x20;               //6 cycles

        //LDA (load accumulator with memory)
        //  Flags: N, Z
        public const byte LDA_IMMEDIATE = 0xA9;     //2 cycles
        public const byte LDA_ZEROPAGE = 0xA5;      //3 cycles
        public const byte LDA_ZEROPAGE_X = 0xB5;    //4 cycles
        public const byte LDA_ABSOLUTE = 0xAD;      //4 cycles
        public const byte LDA_ABSOLUTE_X = 0xBD;    //4 cycles (+1 if page boundary crossed)
        public const byte LDA_ABSOLUTE_Y = 0xB9;    //4 cycles (+1 if page boundary crossed)
        public const byte LDA_INDIRECT_X = 0xA1;    //6 cycles
        public const byte LDA_INDIRECT_Y = 0xB1;    //5 cycles (+1 if page boundary crossed)

        //LDX (load index X with memory)
        //  Flags: N, Z
        public const byte LDX_IMMEDIATE = 0xA2;     //2 cycles
        public const byte LDX_ZEROPAGE = 0xA6;      //3 cycles
        public const byte LDX_ZEROPAGE_Y = 0xB6;    //4 cycles
        public const byte LDX_ABSOLUTE = 0xAE;      //4 cycles
        public const byte LDX_ABSOLUTE_Y = 0xBE;    //4 cycles (+1 if page boundary crossed)

        //LDY (load index Y with memory)
        //  Flags: N, Z
        public const byte LDY_IMMEDIATE = 0xA0;     //2 cycles
        public const byte LDY_ZEROPAGE = 0xA4;      //3 cycles
        public const byte LDY_ZEROPAGE_X = 0xB4;    //4 cycles
        public const byte LDY_ABSOLUTE = 0xAC;      //4 cycles
        public const byte LDY_ABSOLUTE_X = 0xBC;    //4 cycles (+1 if page boundary crossed)

        //LSR (shift one bit right (memory or accumulator))
        //  Flags: Z, C
        public const byte LSR_ACCUMULATOR = 0x4A;   //2 cycles
        public const byte LSR_ZEROPAGE = 0x46;      //5 cycles
        public const byte LSR_ZEROPAGE_X = 0x56;    //6 cycles
        public const byte LSR_ABSOLUTE = 0x4E;      //6 cycles
        public const byte LSR_ABSOLUTE_X = 0x5E;    //7 cycles

        //NOP (no operation)
        public const byte NOP = 0xEA;               //2 cycles

        //ORA (OR memory with accumulator)
        //  Flags: N, Z
        public const byte ORA_IMMEDIATE = 0x09;     //2 cycles
        public const byte ORA_ZEROPAGE = 0x05;      //3 cycles
        public const byte ORA_ZEROPAGE_X = 0x15;    //4 cycles
        public const byte ORA_ABSOLUTE = 0x0D;      //4 cycles
        public const byte ORA_ABSOLUTE_X = 0x1D;    //4 cycles (+1 if page boundary crossed)
        public const byte ORA_ABSOLUTE_Y = 0x19;    //4 cycles (+1 if page boundary crossed)
        public const byte ORA_INDIRECT_X = 0x01;    //6 cycles
        public const byte ORA_INDIRECT_Y = 0x11;    //5 cycles (+1 if page boundary crossed)

        //PHA (push accumulator on stack)
        public const byte PHA = 0x48;               //3 cycles

        //PHP (push processor status on stack)
        public const byte PHP = 0x08;               //3 cycles

        //PLA (pull accumulator from stack)
        //  Flags: N, Z
        public const byte PLA = 0x68;               //4 cycles

        //PLP (pull processor status from stack)
        //   Flags: ALL
        public const byte PLP = 0x28;               //4 cycles

        //ROL (rotate one bit left (memory or accumulator)
        //  Flags: N, Z, C
        public const byte ROL_ACCUMULATOR = 0x2A;   //2 cycles
        public const byte ROL_ZEROPAGE = 0x26;      //5 cycles
        public const byte ROL_ZEROPAGE_X = 0x36;    //6 cycles
        public const byte ROL_ABSOLUTE = 0x2E;      //6 cycles
        public const byte ROL_ABSOLUTE_X = 0x3E;    //7 cycles

        //ROR (rotate one bit right (memory or accumulator)
        //  Flags: N, Z, C
        public const byte ROR_ACCUMULATOR = 0x6A;   //2 cycles
        public const byte ROR_ZEROPAGE = 0x66;      //5 cycles
        public const byte ROR_ZEROPAGE_X = 0x76;    //6 cycles
        public const byte ROR_ABSOLUTE = 0x6E;      //6 cycles
        public const byte ROR_ABSOLUTE_X = 0x7E;    //7 cycles

        //RTI (return from interrupt)
        //  Flags: ALL
        public const byte RTI = 0x40;               //6 cycles

        //RTS (return from subroutine)
        public const byte RTS = 0x60;               //6 cycles

        //SBC (subtract memory from accumulator with borrow)
        //  Flags: N, V, Z, C
        public const byte SBC_IMMEDIATE = 0xE9;     //2 cycles
        public const byte SBC_ZEROPAGE = 0xE5;      //3 cycles
        public const byte SBC_ZEROPAGE_X = 0xF5;    //4 cycles
        public const byte SBC_ABSOLUTE = 0xED;      //4 cycles
        public const byte SBC_ABSOLUTE_X = 0xFD;    //4 cycles (+1 if page boundary crossed)
        public const byte SBC_ABSOLUTE_Y = 0xF9;    //4 cycles (+1 if page boundary crossed)
        public const byte SBC_INDIRECT_X = 0xE1;    //6 cycles
        public const byte SBC_INDIRECT_Y = 0xF1;    //5 cycles (+1 if page boundary crossed)

        //SEC (set carry flag)
        //  Flags: C = 1
        public const byte SEC = 0x38;               //2 cycles

        //SED (set decimal flag)
        //  Flags: D = 1
        public const byte SED = 0xF8;               //2 cycles

        //SEI (set intrurrupt disable flag)
        //  Flags: I = 1
        public const byte SEI = 0x78;               //2 cycles

        //STA (store accumulator in memory)
        public const byte STA_ZEROPAGE = 0x85;      //3 cycles
        public const byte STA_ZEROPAGE_X = 0x95;    //4 cycles
        public const byte STA_ABSOLUTE = 0x8D;      //4 cycles
        public const byte STA_ABSOLUTE_X = 0x9D;    //5 cycles
        public const byte STA_ABSOLUTE_Y = 0x99;    //5 cycles
        public const byte STA_INDIRECT_X = 0x81;    //6 cycles
        public const byte STA_INDIRECT_Y = 0x91;    //6 cycles

        //STX (store index X in memory)
        public const byte STX_ZEROPAGE = 0x86;      //3 cycles
        public const byte STX_ZEROPAGE_Y = 0x96;    //4 cycles
        public const byte STX_ABSOLUTE = 0x8E;      //4 cycles

        //STY (store index Y in memory)
        public const byte STY_ZEROPAGE = 0x84;      //3 cycles
        public const byte STY_ZEROPAGE_X = 0x94;    //4 cycles
        public const byte STY_ABSOLUTE = 0x8C;      //4 cycles

        //TAX (transfer accumulator to index X)
        //  Flags: N, Z
        public const byte TAX = 0xAA;               //2 cycles

        //TAY (transfer accumulator to index Y)
        //  Flags: N, Z
        public const byte TAY = 0xA8;               //2 cycles

        //TSX (transfer stack pointer to index X)
        //  Flags: N, Z
        public const byte TSX = 0xBA;               //2 cycles

        //TXA (transfer index X to accumulator)
        //  Flags: N, Z
        public const byte TXA = 0x8A;               //2 cycles

        //TXS (transfer index X to stack pointer)
        //  Flags: N, Z
        public const byte TXS = 0x9A;               //2 cycles

        //TYA (transfer index Y to accumulator)
        //  Flags: N, Z
        public const byte TYA = 0x98;               //2 cycles

    }
}

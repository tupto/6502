using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6502
{
    public class CPU
    {
        byte A;     //8 bit accumulator register
        byte X;     //8 bit index register
        byte Y;     //8 bit index register
        byte SP;  //16 bit stack pointer but only uses the range $0100 - $01FF   TODO: Consider changing this to a byte
        ushort PC;  //16 bit program counter
        byte P;     //8 bit processor flags NV-DBIZC

        byte[] program;
        byte[] memory;

        public void Run()
        {
            while (!GetFlag(ProcessorFlags.B))
            {
                byte currentOpcode = Read(PC++);

                ushort address = GetAddress(currentOpcode);
            }
        }

        private void EvaluateOpcode(byte opcode, ushort address)
        {
            switch (opcode)
            {
                case Opcodes.ADC_IMMEDIATE:
                case Opcodes.ADC_ZEROPAGE:
                case Opcodes.ADC_ZEROPAGE_X:
                case Opcodes.ADC_ABSOLUTE:
                case Opcodes.ADC_ABSOLUTE_X:
                case Opcodes.ADC_ABSOLUTE_Y:
                case Opcodes.ADC_INDIRECT_X:
                case Opcodes.ADC_INDIRECT_Y:
                    byte memoryValue = Read(address);
                    byte newA = (byte)(A + memoryValue + (GetFlag(ProcessorFlags.C) ? 1 : 0));

                    SetFlag(ProcessorFlags.N, (newA & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, newA == 0x00);
                    SetFlag(ProcessorFlags.C, newA < A);
                    SetFlag(ProcessorFlags.V, (((~(A ^ memoryValue)) & (A ^ newA)) & 0x80) == 0x80);

                    A = newA;
                    break;

                case Opcodes.AND_IMMEDIATE:
                case Opcodes.AND_ZEROPAGE:
                case Opcodes.AND_ZEROPAGE_X:
                case Opcodes.AND_ABSOLUTE:
                case Opcodes.AND_ABSOLUTE_X:
                case Opcodes.AND_ABSOLUTE_Y:
                case Opcodes.AND_INDIRECT_X:
                case Opcodes.AND_INDIRECT_Y:
                    memoryValue = Read(address);
                    newA = (byte)(A & memoryValue);

                    SetFlag(ProcessorFlags.N, (newA & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, newA == 0x00);

                    A = newA;
                    break;

                case Opcodes.ASL_ACCUMULATOR:
                case Opcodes.ASL_ZEROPAGE:
                case Opcodes.ASL_ZEROPAGE_X:
                case Opcodes.ASL_ABSOLUTE:
                case Opcodes.ASL_ABSOLUTE_X:
                    if (opcode == Opcodes.ASL_ACCUMULATOR)
                        memoryValue = A;
                    else
                        memoryValue = Read(address);

                    int tmp = memoryValue << 1;
                    SetFlag(ProcessorFlags.C, ((tmp & 0x100) == 0x100));
                    tmp = (tmp & 0xFF);

                    SetFlag(ProcessorFlags.N, (tmp & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, tmp == 0x00);

                    if (opcode == Opcodes.ASL_ACCUMULATOR)
                        A = (byte)tmp;
                    else
                        Write(address, (byte)tmp);

                    break;

                case Opcodes.BCC:
                    if (!GetFlag(ProcessorFlags.C))
                        PC = address;
                    break;

                case Opcodes.BCS:
                    if (GetFlag(ProcessorFlags.C))
                        PC = address;
                    break;

                case Opcodes.BEQ:
                    if (GetFlag(ProcessorFlags.Z))
                        PC = address;
                    break;

                case Opcodes.BIT_ZEROPAGE:
                case Opcodes.BIT_ABSOLUTE:
                    memoryValue = Read(address);

                    byte result = (byte)(A & memoryValue);
                    SetFlag(ProcessorFlags.N, (result & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, result == 0x00);
                    SetFlag(ProcessorFlags.V, (result & 0x40) == 0x40);

                    break;

                case Opcodes.BMI:
                    if (GetFlag(ProcessorFlags.N))
                        PC = address;
                    break;

                case Opcodes.BNE:
                    if (!GetFlag(ProcessorFlags.Z))
                        PC = address;
                    break;

                case Opcodes.BPL:
                    if (!GetFlag(ProcessorFlags.N))
                        PC = address;
                    break;

                case Opcodes.BRK:
                    break;

                case Opcodes.BVC:
                    if (!GetFlag(ProcessorFlags.V))
                        PC = address;
                    break;

                case Opcodes.BVS:
                    if (GetFlag(ProcessorFlags.C))
                        PC = address;
                    break;

                case Opcodes.CLC:
                    SetFlag(ProcessorFlags.C, false);
                    break;

                case Opcodes.CLD:
                    SetFlag(ProcessorFlags.D, false);
                    break;

                case Opcodes.CLI:
                    SetFlag(ProcessorFlags.I, false);
                    break;

                case Opcodes.CLV:
                    SetFlag(ProcessorFlags.V, false);
                    break;

                case Opcodes.CMP_IMMEDIATE:
                case Opcodes.CMP_ZEROPAGE:
                case Opcodes.CMP_ZEROPAGE_X:
                case Opcodes.CMP_ABSOLUTE:
                case Opcodes.CMP_ABSOLUTE_X:
                case Opcodes.CMP_ABSOLUTE_Y:
                case Opcodes.CMP_INDIRECT_X:
                case Opcodes.CMP_INDIRECT_Y:
                    memoryValue = Read(address);
                    tmp = A - memoryValue;

                    SetFlag(ProcessorFlags.N, (tmp & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, tmp == 0x00);
                    SetFlag(ProcessorFlags.C, tmp < 0x100);
                    break;

                case Opcodes.CPX_IMMEDIATE:
                case Opcodes.CPX_ZEROPAGE:
                case Opcodes.CPX_ABSOLUTE:
                    memoryValue = Read(address);
                    tmp = X - memoryValue;

                    SetFlag(ProcessorFlags.N, (tmp & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, tmp == 0x00);
                    SetFlag(ProcessorFlags.C, tmp < 0x100);
                    break;

                case Opcodes.CPY_IMMEDIATE:
                case Opcodes.CPY_ZEROPAGE:
                case Opcodes.CPY_ABSOLUTE:
                    memoryValue = Read(address);
                    tmp = Y - memoryValue;

                    SetFlag(ProcessorFlags.N, (tmp & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, tmp == 0x00);
                    SetFlag(ProcessorFlags.C, tmp < 0x100);
                    break;

                case Opcodes.DEC_ZEROPAGE:
                case Opcodes.DEC_ZEROPAGE_X:
                case Opcodes.DEC_ABSOLUTE:
                case Opcodes.DEC_ABSOLUTE_X:
                    memoryValue = Read(address);
                    tmp = memoryValue - 1;

                    SetFlag(ProcessorFlags.N, (tmp & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, tmp == 0x00);

                    Write(address, (byte)tmp);
                    break;

                case Opcodes.DEX:
                    byte newX = (byte)(X - 1);

                    SetFlag(ProcessorFlags.N, (newX & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, newX == 0x00);

                    X = newX;
                    break;

                case Opcodes.DEY:
                    byte newY = (byte)(Y - 1);

                    SetFlag(ProcessorFlags.N, (newY & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, newY == 0x00);

                    Y = newY;
                    break;

                case Opcodes.EOR_IMMEDIATE:
                case Opcodes.EOR_ZEROPAGE:
                case Opcodes.EOR_ZEROPAGE_X:
                case Opcodes.EOR_ABSOLUTE:
                case Opcodes.EOR_ABSOLUTE_X:
                case Opcodes.EOR_ABSOLUTE_Y:
                case Opcodes.EOR_INDIRECT_X:
                case Opcodes.EOR_INDIRECT_Y:
                    break;

                case Opcodes.INC_ZEROPAGE:
                case Opcodes.INC_ZEROPAGE_X:
                case Opcodes.INC_ABSOLUTE:
                case Opcodes.INC_ABSOLUTE_X:
                    memoryValue = Read(address);
                    tmp = memoryValue + 1;

                    SetFlag(ProcessorFlags.N, (tmp & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, tmp == 0x00);

                    Write(address, (byte)tmp);
                    break;

                case Opcodes.INX:
                    newX = (byte)(X + 1);

                    SetFlag(ProcessorFlags.N, (newX & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, newX == 0x00);

                    X = newX;
                    break;

                case Opcodes.INY:
                    newY = (byte)(Y + 1);

                    SetFlag(ProcessorFlags.N, (newY & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, newY == 0x00);

                    Y = newY;
                    break;

                case Opcodes.JMP_ABSOLUTE:
                case Opcodes.JMP_INDIRECT:
                    PC = address;
                    break;

                case Opcodes.JSR:
                    break;

                case Opcodes.LDA_IMMEDIATE:
                case Opcodes.LDA_ZEROPAGE:
                case Opcodes.LDA_ZEROPAGE_X:
                case Opcodes.LDA_ABSOLUTE:
                case Opcodes.LDA_ABSOLUTE_X:
                case Opcodes.LDA_ABSOLUTE_Y:
                case Opcodes.LDA_INDIRECT_X:
                case Opcodes.LDA_INDIRECT_Y:
                    break;

                case Opcodes.LDX_IMMEDIATE:
                case Opcodes.LDX_ZEROPAGE:
                case Opcodes.LDX_ZEROPAGE_Y:
                case Opcodes.LDX_ABSOLUTE:
                case Opcodes.LDX_ABSOLUTE_Y:
                    break;

                case Opcodes.LDY_IMMEDIATE:
                case Opcodes.LDY_ZEROPAGE:
                case Opcodes.LDY_ZEROPAGE_X:
                case Opcodes.LDY_ABSOLUTE:
                case Opcodes.LDY_ABSOLUTE_X:
                    break;

                case Opcodes.LSR_ACCUMULATOR:
                case Opcodes.LSR_ZEROPAGE:
                case Opcodes.LSR_ZEROPAGE_X:
                case Opcodes.LSR_ABSOLUTE:
                case Opcodes.LSR_ABSOLUTE_X:
                    break;

                case Opcodes.NOP:
                    break;

                case Opcodes.ORA_IMMEDIATE:
                case Opcodes.ORA_ZEROPAGE:
                case Opcodes.ORA_ZEROPAGE_X:
                case Opcodes.ORA_ABSOLUTE:
                case Opcodes.ORA_ABSOLUTE_X:
                case Opcodes.ORA_ABSOLUTE_Y:
                case Opcodes.ORA_INDIRECT_X:
                case Opcodes.ORA_INDIRECT_Y:
                    break;

                case Opcodes.PHA:
                    break;

                case Opcodes.PHP:
                    break;

                case Opcodes.PLA:
                    break;

                case Opcodes.PLP:
                    break;

                case Opcodes.ROL_ACCUMULATOR:
                case Opcodes.ROL_ZEROPAGE:
                case Opcodes.ROL_ZEROPAGE_X:
                case Opcodes.ROL_ABSOLUTE:
                case Opcodes.ROL_ABSOLUTE_X:
                    break;

                case Opcodes.ROR_ACCUMULATOR:
                case Opcodes.ROR_ZEROPAGE:
                case Opcodes.ROR_ZEROPAGE_X:
                case Opcodes.ROR_ABSOLUTE:
                case Opcodes.ROR_ABSOLUTE_X:
                    break;

                case Opcodes.RTI:
                    break;

                case Opcodes.RTS:
                    break;

                case Opcodes.SBC_IMMEDIATE:
                case Opcodes.SBC_ZEROPAGE:
                case Opcodes.SBC_ZEROPAGE_X:
                case Opcodes.SBC_ABSOLUTE:
                case Opcodes.SBC_ABSOLUTE_X:
                case Opcodes.SBC_ABSOLUTE_Y:
                case Opcodes.SBC_INDIRECT_X:
                case Opcodes.SBC_INDIRECT_Y:
                    break;

                case Opcodes.SEC:
                    break;

                case Opcodes.SED:
                    break;

                case Opcodes.SEI:
                    break;

                case Opcodes.STA_ZEROPAGE:
                case Opcodes.STA_ZEROPAGE_X:
                case Opcodes.STA_ABSOLUTE:
                case Opcodes.STA_ABSOLUTE_X:
                case Opcodes.STA_ABSOLUTE_Y:
                case Opcodes.STA_INDIRECT_X:
                case Opcodes.STA_INDIRECT_Y:
                    memory[address] = A;
                    break;

                case Opcodes.STX_ZEROPAGE:
                case Opcodes.STX_ZEROPAGE_Y:
                case Opcodes.STX_ABSOLUTE:
                    memory[address] = X;
                    break;

                case Opcodes.STY_ZEROPAGE:
                case Opcodes.STY_ZEROPAGE_X:
                case Opcodes.STY_ABSOLUTE:
                    memory[address] = Y;
                    break;

                case Opcodes.TAX:
                    X = A;

                    SetFlag(ProcessorFlags.N, (X & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, X == 0x00);
                    break;

                case Opcodes.TAY:
                    Y = A;

                    SetFlag(ProcessorFlags.N, (Y & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, Y == 0x00);
                    break;

                case Opcodes.TSX:
                    SP = A;

                    SetFlag(ProcessorFlags.N, (SP & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, SP == 0x00);
                    break;

                case Opcodes.TXA:
                    A = X;

                    SetFlag(ProcessorFlags.N, (A & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, A == 0x00);
                    break;

                case Opcodes.TXS:
                    SP = X;

                    SetFlag(ProcessorFlags.N, (SP & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, SP == 0x00);
                    break;

                case Opcodes.TYA:
                    A = Y;

                    SetFlag(ProcessorFlags.N, (A & 0x80) == 0x80);
                    SetFlag(ProcessorFlags.Z, A == 0x00);
                    break;

                default:
                    throw new Exception("Invalid opcode");

            }
        }

        #region Addressing functions
        public ushort GetImmediate()
        {
            return PC++;
        }

        public ushort GetZeropage()
        {
            return Read(PC++);
        }

        public ushort GetZeropageX()
        {
            return (ushort)(Read(PC++) + X);
        }

        public ushort GetZeropageY()
        {
            return (ushort)(Read(PC++) + Y);
        }

        public ushort GetAbsolute()
        {
            byte lowNibble = Read(PC++);
            byte highNibble = Read(PC++);

            return (ushort)((highNibble << 8) + lowNibble);
        }

        public ushort GetAbsoluteX()
        {
            byte lowNibble = Read(PC++);
            byte highNibble = Read(PC++);

            return (ushort)((highNibble << 8) + lowNibble + X + (GetFlag(ProcessorFlags.C) ? 1 :0));
        }

        public ushort GetAbsoluteY()
        {
            byte lowNibble = Read(PC++);
            byte highNibble = Read(PC++);

            return (ushort)((highNibble << 8) + lowNibble + Y + (GetFlag(ProcessorFlags.C) ? 1 : 0));
        }

        public ushort GetRelative()
        {
            byte b = Read(PC++);

            //TODO: make sure the negative values work correctly
            return (ushort)(PC + (sbyte)b);
        }

        public ushort GetIndirect()
        {
            byte lowNibble = Read(PC++);
            byte highNibble = Read(PC++);

            ushort indirectAddress = (ushort)((highNibble << 8) + lowNibble);

            lowNibble = Read(indirectAddress);
            highNibble = Read((ushort)(indirectAddress + 1));

            return (ushort)((highNibble << 8) + lowNibble);
        }

        public ushort GetIndirectX()
        {
            return (ushort)((Read(PC++) + X) % 256);
        }

        public ushort GetIndirectY()
        {
            return (ushort)((Read(PC++) + Y + (GetFlag(ProcessorFlags.C) ? 1 : 0)) % 256);
        }

        public ushort GetAddress(byte opcode)
        {
            switch (opcode)
            {
                case Opcodes.ADC_ABSOLUTE:
                case Opcodes.AND_ABSOLUTE:
                case Opcodes.ASL_ABSOLUTE:
                case Opcodes.BIT_ABSOLUTE:
                case Opcodes.CMP_ABSOLUTE:
                case Opcodes.CPX_ABSOLUTE:
                case Opcodes.CPY_ABSOLUTE:
                case Opcodes.DEC_ABSOLUTE:
                case Opcodes.EOR_ABSOLUTE:
                case Opcodes.INC_ABSOLUTE:
                case Opcodes.JMP_ABSOLUTE:
                case Opcodes.LDA_ABSOLUTE:
                case Opcodes.LDX_ABSOLUTE:
                case Opcodes.LDY_ABSOLUTE:
                case Opcodes.LSR_ABSOLUTE:
                case Opcodes.ORA_ABSOLUTE:
                case Opcodes.ROL_ABSOLUTE:
                case Opcodes.ROR_ABSOLUTE:
                case Opcodes.SBC_ABSOLUTE:
                case Opcodes.STA_ABSOLUTE:
                case Opcodes.STX_ABSOLUTE:
                case Opcodes.STY_ABSOLUTE:
                    return GetAbsolute();

                case Opcodes.ADC_ABSOLUTE_X:
                case Opcodes.AND_ABSOLUTE_X:
                case Opcodes.ASL_ABSOLUTE_X:
                case Opcodes.CMP_ABSOLUTE_X:
                case Opcodes.DEC_ABSOLUTE_X:
                case Opcodes.EOR_ABSOLUTE_X:
                case Opcodes.INC_ABSOLUTE_X:
                case Opcodes.LDA_ABSOLUTE_X:
                case Opcodes.LDY_ABSOLUTE_X:
                case Opcodes.LSR_ABSOLUTE_X:
                case Opcodes.ORA_ABSOLUTE_X:
                case Opcodes.ROL_ABSOLUTE_X:
                case Opcodes.ROR_ABSOLUTE_X:
                case Opcodes.SBC_ABSOLUTE_X:
                case Opcodes.STA_ABSOLUTE_X:
                    return GetAbsoluteX();

                case Opcodes.ADC_ABSOLUTE_Y:
                case Opcodes.AND_ABSOLUTE_Y:
                case Opcodes.CMP_ABSOLUTE_Y:
                case Opcodes.EOR_ABSOLUTE_Y:
                case Opcodes.LDA_ABSOLUTE_Y:
                case Opcodes.LDX_ABSOLUTE_Y:
                case Opcodes.ORA_ABSOLUTE_Y:
                case Opcodes.SBC_ABSOLUTE_Y:
                case Opcodes.STA_ABSOLUTE_Y:
                    return GetAbsoluteY();

                case Opcodes.ADC_IMMEDIATE:
                case Opcodes.AND_IMMEDIATE:
                case Opcodes.CMP_IMMEDIATE:
                case Opcodes.CPX_IMMEDIATE:
                case Opcodes.CPY_IMMEDIATE:
                case Opcodes.EOR_IMMEDIATE:
                case Opcodes.LDA_IMMEDIATE:
                case Opcodes.LDX_IMMEDIATE:
                case Opcodes.LDY_IMMEDIATE:
                case Opcodes.ORA_IMMEDIATE:
                case Opcodes.SBC_IMMEDIATE:
                    return GetImmediate();

                case Opcodes.JMP_INDIRECT:
                    return GetIndirect();

                case Opcodes.ADC_INDIRECT_X:
                case Opcodes.AND_INDIRECT_X:
                case Opcodes.CMP_INDIRECT_X:
                case Opcodes.EOR_INDIRECT_X:
                case Opcodes.LDA_INDIRECT_X:
                case Opcodes.ORA_INDIRECT_X:
                case Opcodes.SBC_INDIRECT_X:
                case Opcodes.STA_INDIRECT_X:
                    return GetIndirectX();

                case Opcodes.ADC_INDIRECT_Y:
                case Opcodes.AND_INDIRECT_Y:
                case Opcodes.CMP_INDIRECT_Y:
                case Opcodes.EOR_INDIRECT_Y:
                case Opcodes.LDA_INDIRECT_Y:
                case Opcodes.ORA_INDIRECT_Y:
                case Opcodes.SBC_INDIRECT_Y:
                case Opcodes.STA_INDIRECT_Y:
                    return GetIndirectY();

                case Opcodes.ADC_ZEROPAGE:
                case Opcodes.AND_ZEROPAGE:
                case Opcodes.ASL_ZEROPAGE:
                case Opcodes.BIT_ZEROPAGE:
                case Opcodes.CMP_ZEROPAGE:
                case Opcodes.CPX_ZEROPAGE:
                case Opcodes.CPY_ZEROPAGE:
                case Opcodes.DEC_ZEROPAGE:
                case Opcodes.EOR_ZEROPAGE:
                case Opcodes.INC_ZEROPAGE:
                case Opcodes.LDA_ZEROPAGE:
                case Opcodes.LDX_ZEROPAGE:
                case Opcodes.LDY_ZEROPAGE:
                case Opcodes.LSR_ZEROPAGE:
                case Opcodes.ORA_ZEROPAGE:
                case Opcodes.ROL_ZEROPAGE:
                case Opcodes.ROR_ZEROPAGE:
                case Opcodes.SBC_ZEROPAGE:
                case Opcodes.STA_ZEROPAGE:
                case Opcodes.STX_ZEROPAGE:
                case Opcodes.STY_ZEROPAGE:
                    return GetZeropage();

                case Opcodes.ADC_ZEROPAGE_X:
                case Opcodes.AND_ZEROPAGE_X:
                case Opcodes.ASL_ZEROPAGE_X:
                case Opcodes.CMP_ZEROPAGE_X:
                case Opcodes.DEC_ZEROPAGE_X:
                case Opcodes.EOR_ZEROPAGE_X:
                case Opcodes.INC_ZEROPAGE_X:
                case Opcodes.LDA_ZEROPAGE_X:
                case Opcodes.LDY_ZEROPAGE_X:
                case Opcodes.LSR_ZEROPAGE_X:
                case Opcodes.ORA_ZEROPAGE_X:
                case Opcodes.ROL_ZEROPAGE_X:
                case Opcodes.ROR_ZEROPAGE_X:
                case Opcodes.SBC_ZEROPAGE_X:
                case Opcodes.STA_ZEROPAGE_X:
                case Opcodes.STY_ZEROPAGE_X:
                    return GetZeropageX();

                case Opcodes.LDX_ZEROPAGE_Y:
                case Opcodes.STX_ZEROPAGE_Y:
                    return GetZeropageY();

                default:
                    return 0;
            }
        }
        #endregion

        #region Processor flag IO
        private bool GetFlag(ProcessorFlags flag)
        {
            return ((P >> (byte)flag) & 1) == 1;
        }

        private void SetFlag(ProcessorFlags flag, bool value)
        {
            P ^= (byte)((-(value ? 1 : 0) ^ P) & (1 << (byte)flag));
        }
        #endregion

        #region Memory IO
        private byte Read(ushort address)
        {
            return memory[address];
        }

        private void Write(ushort address, byte value)
        {
            memory[address] = value;
        }
        #endregion

        private enum ProcessorFlags
        {
            N = 7,  //Negative flag
            V = 6,  //Overflow flag
            D = 4,  //Decimal mode
            B = 3,  //Break command
            I = 2,  //Interrupt disable
            Z = 1,  //Zero flag
            C = 0   //Carry flag
        }
    }
}

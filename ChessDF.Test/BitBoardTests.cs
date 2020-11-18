﻿using ChessDF.Core;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChessDF.Test
{
    public class BitBoardTests
    {
        [Theory]
        [InlineData(0x00_00_00_00_00_00_00_00, 0)]
        [InlineData(0x10_00_00_00_00_00_00_00, 1)]
        [InlineData(0x10_00_10_00_00_10_00_00, 3)]
        [InlineData(0xFF_FF_FF_FF_FF_FF_FF_FF, 64)]
        [InlineData(0xFF_FF_FF_FF_FF_FF_01_FF, 57)]
        public void PopCountIsAsExpected(ulong bits, int expectedCount)
        {
            // Assemble
            var board = new Bitboard(bits);

            // Act
            int count = board.PopCount();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Theory]
        [InlineData(0xAB_00_12_45_00_0F_00_EE, 0xEE_00_0F_00_45_12_00_AB)]
        public void CanFlipVertically(ulong startBits, ulong flippedBits)
        {
            // Assemble
            var board = new Bitboard(startBits);

            // Act
            Bitboard flipped = board.FlipVertical();

            // Assert
            Assert.Equal(new Bitboard(flippedBits), flipped);
        }

        [Fact]
        public void CanSerializeBoard()
        {
            // Assemble
            var board = new Bitboard(0x00_00_00_00_00_00_01_01);

            // Act
            List<int> bitIndices = board.Serialize();

            // Assert
            var expected = new List<int> { 0, 8 };
            bitIndices.Should().BeEquivalentTo(expected);
        }
    }
}
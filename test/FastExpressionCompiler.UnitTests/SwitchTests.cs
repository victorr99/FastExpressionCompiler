using System;
using System.Diagnostics;

#if LIGHT_EXPRESSION
using static FastExpressionCompiler.LightExpression.Expression;
namespace FastExpressionCompiler.LightExpression.UnitTests
#else
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
namespace FastExpressionCompiler.UnitTests
#endif
{
    public class SwitchTests : ITest
    {
        // Test cases:
        // types: byte, sbyte, short, ushort, int, uint, long, ulong
        // 
        // case count: 6, 7, 8
        // case density: 40, 60, 100
        // overflow is not supported 

        public int Run()
        {
            Emit_switch_opcode_for_8_cases();

            return 1;
        }

        public void Emit_switch_opcode_for_8_cases()
        {
            var parameter = Parameter(typeof(int));
            
            var expression = Lambda<Func<int, int>>(
                Switch(
                    parameter,
                    Constant(-1),
                    SwitchCase(
                        Constant(0),
                        Constant(0)),
                    SwitchCase(
                        Constant(1),
                        Constant(1)),
                    SwitchCase(
                        Constant(2),
                        Constant(2)),
                    SwitchCase(
                        Constant(3),
                        Constant(3)),
                    SwitchCase(
                        Constant(4),
                        Constant(4)),
                    SwitchCase(
                        Constant(5),
                        Constant(5)),
                    SwitchCase(
                        Constant(6),
                        Constant(6)),
                    SwitchCase(
                        Constant(7),
                        Constant(7))),
                parameter);

            var dlg = expression.CompileFast<Func<int, int>>();
            dlg.PrintIL();

            Asserts.IsNotNull(dlg);
            Asserts.AreEqual(5, dlg(5));
        }
    }
}

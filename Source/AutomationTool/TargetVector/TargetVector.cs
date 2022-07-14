using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTool;

namespace TargetVector.Automation
{
	// Use [Help()] attributes to document your command and its arguments.
	[Help("Sample script printing a Fibonacci sequence of values.")]
	[Help("Usage: SampleScript -Terms=<N>")]
	[Help("Terms=<N>", "N (int) represents how many terms to compute, and it must be greater than or equal to 1.")]

	// BuildCommand is the base class for all commands.
	public class TargetVector : BuildCommand
	{
		public override void ExecuteBuild()
		{
			// The ParseParamInt() method retrieves a command line argument for this example. ParseParam() retrieves a bool, and ParseParamValue retrieves a string.
			int NumTerms = ParseParamInt("Terms");
			if (NumTerms < 1)
			{
				throw new AutomationException("Invalid number of terms specified. Enter -help for syntax.");
			}
			else
			{
				LogInformation("Fibonnacci sequence:");

				int TermA = 1;
				int TermB = 1;
				for (int TermIdx = 0; TermIdx < NumTerms; TermIdx++)
				{
					LogInformation("  {0}", TermA);

					int NextTerm = TermA + TermB;
					TermA = TermB;
					TermB = NextTerm;
				}
			}
		}
	}
}
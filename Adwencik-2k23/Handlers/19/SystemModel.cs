using Adwencik_2k23.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Adwencik_2k23.Handlers._19.RequirementModel;

namespace Adwencik_2k23.Handlers._19
{
    internal enum Selector
    {
        X,
        M,
        A,
        S
    }

    internal class SystemModel
    {

    }

    internal class WorkflowModel
    {
        public WorkflowProperty[] Properties { get; set; }
        public RequirementModel[] Requirements { get; set; }

        public WorkflowModel(string[] input)
        {
            var groupBySplit = input.BatchWhere(c => c == "");

            Requirements = groupBySplit
                .First()
                .Select(input =>
                {
                    var split = input.Split("{");
                    var name = split[0];

                    var reqs = split[1].Replace("}", "");
                    return new RequirementModel(name, reqs);
                }).ToArray();   

            Properties = groupBySplit
                .Last()
                .SelectMany(input => {
                    input = input.Replace("}", "").Replace("}", "");
                    var values = input.Split(",");
                    return values.Select(inp =>
                    {
                        var cos = inp.Split("=");
                        return new WorkflowProperty(cos[0], cos[1]);
                    });
                }).ToArray();
        }
    }

    internal class WorkflowProperty 
    {
        public Selector Selector { get; set; }
        public int Value { get; set; }

        public WorkflowProperty(string selector, string value)
        {
            Selector = GetSelector(selector);
            Value = int.Parse(value);
        }

        internal Selector GetSelector(string selector)
            => selector switch
            {
                "x" => Selector.X,
                "m" => Selector.M,
                "a" => Selector.A,
                _ => Selector.S,
            };
    }

    internal class RequirementModel
    {
        public string Name { get; set; }
        public Requirement[] Requirements { get; set; }

        public RelationalOperator(string name, string requirements)
        {
            Name = name;

        }

        internal enum RelationalOperator
        {
            Less,
            More
        }
    }

    internal class Requirement
    {
        public int Value { get; set; }
        public RelationalOperator Operator { get; set; }

        public bool IsAccepted(WorkflowProperty property)
            => Operator switch
            {
                RelationalOperator.Less => property.Value < Value,
                _ => property.Value > Value
            };

        public Requirement(string input)
        {
            
        }
    }
}

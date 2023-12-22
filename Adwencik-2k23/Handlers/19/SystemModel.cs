using Adwencik_2k23.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Adwencik_2k23.Handlers._19.WorkflowModel;

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
        public RatingProperty[][] Properties { get; set; }
        public WorkflowModel[] Workflows { get; set; }

        public SystemModel(string[] input)
        {
            var groupBySplit = input.BatchWhere(c => c == "");

            Workflows = groupBySplit
                .First()
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(input =>
                {
                    var split = input.Split("{");
                    var name = split[0];

                    var reqs = split[1].Replace("}", "");
                    return new WorkflowModel(name, reqs);
                }).ToArray();   

            Properties = groupBySplit
                .Last()
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(input => {
                    input = input.Replace("}", "").Replace("{", "");
                    var values = input.Split(",");
                    return values.Select(inp =>
                    {
                        var cos = inp.Split("=");
                        return new RatingProperty(cos[0], cos[1]);
                    }).ToArray();
                }).ToArray();
        }

        public int SolveFirst()
        {
            var counter = 0;
            foreach(var propertyGroup in Properties)
            {
                var firstWorkflow = Workflows.First(w => w.Name == "in");
                counter += IsPropertyGroupAccepted(propertyGroup, firstWorkflow) ? propertyGroup.Sum(p => p.Value) : 0;
            }

            return counter;
        }

        public long SolveSecond()
        {
            var dupa = Workflows
                .Select(c => c.Requirements.Mul(r =>
                    (long)(r.Operator switch
                    {
                        Requirement.RelationalOperator.Less => r.Value,
                        _ => 4000 - r.Value
                    })
                ));

            return Workflows
                .Mul(c => c.Requirements.Sum(r =>
                    (long)(r.Operator switch
                    {
                        Requirement.RelationalOperator.Less => r.Value,
                        _ => 4000 - r.Value
                    })
                ));
        }

        private bool IsPropertyGroupAccepted(RatingProperty[] properties, WorkflowModel workflow)
        {
            foreach (var requirement in workflow.Requirements)
            {
                var property = properties.First(p => p.Selector == requirement.Selector);

                if (requirement.IsAccepted(property))
                {
                    var nextWorkflow = Workflows.FirstOrDefault(w => w.Name == requirement.AcceptedWorkflowName);

                    if (nextWorkflow is not null)
                        return IsPropertyGroupAccepted(properties, nextWorkflow);

                    return requirement.AcceptedWorkflowName == "A";
                }
            }

            var nextnoHitWorkflow = Workflows.FirstOrDefault(w => w.Name == workflow.NoHitWorkflowName);

            if (nextnoHitWorkflow is not null)
                return IsPropertyGroupAccepted(properties, nextnoHitWorkflow);

            return workflow.NoHitWorkflowName == "A";
        }
    }

    internal class RatingProperty 
    {
        public Selector Selector { get; set; }
        public int Value { get; set; }

        public RatingProperty(string selector, string value)
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

    internal class WorkflowModel
    {
        public string Name { get; set; }
        public Requirement[] Requirements { get; set; }
        public string NoHitWorkflowName { get; set; }

        public WorkflowModel(string name, string requirements)
        {
            Name = name;
            var splitByRequirements = requirements
                .Split(",");

            Requirements = splitByRequirements
                .Take(splitByRequirements.Length - 1)
                .Select(l => new Requirement(l))
                .ToArray();

            NoHitWorkflowName = splitByRequirements.Last();
        }
    }

    internal class Requirement
    {
        public int Value { get; set; }
        public Selector Selector { get; set; }
        public RelationalOperator Operator { get; set; }
        public string AcceptedWorkflowName { get; set; }

        private readonly Dictionary<char, RelationalOperator> _charToOperatorMap = new Dictionary<char, RelationalOperator>()
        {
            { '>', RelationalOperator.More },
            { '<', RelationalOperator.Less },
        };

        internal Selector GetSelector(string selector)
            => selector switch
            {
                "x" => Selector.X,
                "m" => Selector.M,
                "a" => Selector.A,
                _ => Selector.S,
            };

        public bool IsAccepted(RatingProperty property)
            => Operator switch
            {
                RelationalOperator.Less => property.Value < Value,
                _ => property.Value > Value
            };

        public Requirement(string input)
        {
            var split = input.Split(":");
            AcceptedWorkflowName = split.Last();
            Operator = _charToOperatorMap[split.First()[1]];
            Selector = GetSelector(split.First()[0].ToString());
            Value = int.Parse(split.First().Substring(2));
        }

        internal enum RelationalOperator
        {
            Less,
            More
        }
    }
}

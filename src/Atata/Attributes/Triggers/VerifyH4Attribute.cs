namespace Atata
{
    /// <summary>
    /// Indicates the verification of the content of the &lt;h4&gt; tag when the page object is initialized.
    /// </summary>
    public class VerifyH4Attribute : VerifyHeadingTriggerAttribute
    {
        public VerifyH4Attribute(TermCase termCase = TermCase.Inherit)
            : base(termCase)
        {
        }

        public VerifyH4Attribute(TermMatch match, TermCase termCase = TermCase.Inherit)
            : base(match, termCase)
        {
        }

        public VerifyH4Attribute(TermMatch match, params string[] values)
            : base(match, values)
        {
        }

        public VerifyH4Attribute(params string[] values)
            : base(values)
        {
        }

        protected override void OnExecute<TOwner>(TriggerContext<TOwner> context, string[] values)
        {
            string name = TermResolver.ToDisplayString(values);
            var headingControl = context.Component.Owner.CreateControl<H4<TOwner>>(name, new FindByIndexAttribute(Index));
            headingControl.VerifyUntilMatchesAny(Match, values);
        }
    }
}

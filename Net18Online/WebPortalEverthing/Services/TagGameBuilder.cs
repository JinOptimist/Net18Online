using Everything.Data.Interface.Repositories;
using TagGame.Classes.Builders;

namespace WebPortalEverthing.Services
{
    public class TagGameBuilder
    {
        private Builder _builder;

        public TagGameBuilder(Builder builder)
        {
            _builder = builder;
        }

        public void Create()
        {
            _builder = new Builder();
            _builder.Build();
        }

        public Builder GetBuilder()
        {
            return _builder;
        }
    }
}

using System;
using System.Linq;
using GraphQL;
using GraphQL.EntityFramework;

[GraphQLMetadata("Parent")]
public class ParentGraph :
    EfObjectGraphType<IntegrationDbContext, ParentEntity>
{
    public ParentGraph(IEfGraphQLService<IntegrationDbContext> graphQlService) :
        base(graphQlService)
    {
        AddNavigationConnectionField(
            name: "childrenConnection",
            resolve: context => context.Source.Children,
            includeNames: new[] {"Children"});
        AddNavigationField(
            name: "nullNavigationChild",
            resolve: context =>
            {
                return context.Source.Children.FirstOrDefault(x => x.Id == Guid.Empty);
            },
            includeNames: new[] {"Children"});
        AutoMap();
    }
}
using System.Threading;
using System.Threading.Tasks;
using Gitregator.Github.Client;
using Octokit;
using AutoFixture;
using Gitregator.Api.Dtos;
using User = Octokit.User;

namespace Gitregator.Api.Services;

public sealed class GithubAggregatorService : IGithubAggregatorService
{
    private readonly GithubClientProvider _provider;
    private readonly IFixture _fixture;

    public GithubAggregatorService(GithubClientProvider provider)
    {
        _provider = provider;
        _fixture = new Fixture();
    }

    public Task<GetRepositoryAggregationResponse> GetRepositoryAggregationAsync(
        string githubUrl,
        CancellationToken cancellationToken)
        => Task.FromResult(
            new GetRepositoryAggregationResponse
            {
                RepositoryName = "HelloWorld",
                RepositoryHttpUrl = "https://github.com/itatmisis/gitregator",
                RepositoryOwner = "SomeOrganization",
                RepositoryDescription = "Some description of repository",
                Collaborators = _fixture.CreateMany<Collaborator>(),
                Issuers = _fixture.CreateMany<Collaborator>()
            });

    public Task<User> GetMemberAggregationAsync(string userLogin, CancellationToken cancellationToken)
        => _provider.GetClient().User.Get(userLogin).WaitAsync(cancellationToken);
}

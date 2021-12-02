# Set-up
find . -type d -name "Generated" | xargs rm -rf

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" synionnl/csharp-generator-cli generate \
    -i https://raw.githubusercontent.com/synionnl/website/${VERSION}/docs/blog/bizdevops/living-documentation/process.bpmn \
    -o /local/Generated \
    --namespace Adapters.Zeebe.Generated

# Set-up
find . -type d -name "Generated" | xargs rm -rf

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" camunda-community-hub/csharp-generator-cli generate \
    -i https://www.synion.nl/blog/bizdevops/living-documentation/process.bpmn \
    -o /local/Generated \
    --namespace Adapters.Zeebe.Generated
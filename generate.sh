# Set-up
find . -type d -name "Generated" | xargs rm -rf

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}/Adapters:/local" openapitools/openapi-generator-cli generate \
    -i https://www.synion.nl/blog/bizdevops/living-documentation/openapi.yaml \
    -g aspnetcore \
    -o /local/Generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=Adapters.Rest.Generated,swashbuckleVersion=5.0.0

# Copy generated .cs files
mkdir Adapters/Adapters.Rest/Generated
find Adapters/Generated/src/Adapters.Rest.Generated -type f -name '*.cs' | 
    while read P; do cp --parents "$P" Adapters/Adapters.Rest/Generated; done

# # Clean up
rm -rf ./Adapters/Generated
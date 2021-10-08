# Set-up
find . -type d -name "Generated" | xargs rm -rf

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}/Adapters:/local" openapitools/openapi-generator-cli generate \
    -i https://www.synion.nl/blog/bizdevops/living-documentation/openapi.yaml \
    -g aspnetcore \
    -o /local/Generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=Adapters.Rest.Generated,swashbuckleVersion=5.0.0


# Copy generated
cp -r ./Adapters/Generated/src/Adapters.Rest.Generated ./Adapters/Adapters.Rest/Generated

# Clean up
rm -rf ./Adapters/Generated
rm -rf ./Adapters/Adapters.Rest/Generated/Adapters.Rest.Generated.csproj
rm -rf ./Adapters/Adapters.Rest/Generated/Adapters.Rest.Generated.nuspec
rm -rf ./Adapters/Adapters.Rest/Generated/.gitignore
rm -rf ./Adapters/Adapters.Rest/Generated/bin
rm -rf ./Adapters/Adapters.Rest/Generated/obj
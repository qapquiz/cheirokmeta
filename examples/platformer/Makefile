SERVER_OUT := "platformer.sh"
CSHARP_OUT_1 := "./builds/csharp/Platformer.cs"
CSHARP_OUT_2 := "./builds/csharp/PlatformerGrpc.cs"
GO_OUT := "./builds/golang/platformer.pb.go"

.PHONY: all client api clean help

all: client api # Auto-generate grpc C# and go source

client: build/platformer-client ## Auto-generate grpc C# source

api: build/platformer ## AUto-generate grpc go sources

build/platformer-client: platformer.proto
	@protoc -I ./ \
			--csharp_out ./builds/csharp \
			--grpc_out ./builds/csharp \
			--plugin=protoc-gen-grpc=./Grpc.Tools.1.16.0/tools/macosx_x64/grpc_csharp_plugin \
			platformer.proto	

build/platformer: platformer.proto
	@protoc -I ./ \
			-I ${GOPATH}/src \
			--go_out=plugins=grpc:./builds/golang \
			platformer.proto

clean: ## Remove previous builds
	@rm $(CSHARP_OUT_1)
	@rm $(CSHARP_OUT_2)
	@rm $(GO_OUT)

help: ## Display this help screen
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'

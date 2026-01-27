package main

import (
	"context"
	"log"
	"os"
	"os/signal"
	"syscall"

	"github.com/canopy-network/go-plugin/contract"
)

const PlaygroundVersion = "v0.1.0"

func main() {
	log.Printf("Canopy Plugin Playground %s (Go)", PlaygroundVersion)
	// start the plugin
	contract.StartPlugin(contract.DefaultConfig())
	// create a cancellable context that listens for kill signals
	ctx, stop := signal.NotifyContext(context.Background(), os.Interrupt, syscall.SIGTERM)
	defer stop()
	<-ctx.Done()
}

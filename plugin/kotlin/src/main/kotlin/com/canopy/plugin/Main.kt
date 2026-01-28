package com.canopy.plugin

import mu.KotlinLogging
import java.util.concurrent.CountDownLatch

private val logger = KotlinLogging.logger {}

private const val PLAYGROUND_VERSION = "v0.1.0"

/**
 * Main entry point for the Canopy Plugin
 * Matches Go implementation simplicity
 */
fun main() {
    logger.info { "Canopy Plugin Playground $PLAYGROUND_VERSION (Kotlin) - PABLO WAS HERE" }

    // Start the plugin with default config
    val config = Config.default()
    val plugin = PluginClient(config)
    plugin.start()

    // Wait for shutdown signal
    val shutdownLatch = CountDownLatch(1)
    Runtime.getRuntime().addShutdownHook(Thread {
        logger.info { "Received shutdown signal" }
        plugin.close()
        shutdownLatch.countDown()
    })

    shutdownLatch.await()
}

import { StartPlugin, DefaultConfig, initializeContract } from './contract/plugin.js';
import { Contract, ContractConfig, ContractAsync } from './contract/contract.js';

const PLAYGROUND_VERSION = 'v0.1.0';

console.log(`Canopy Plugin Playground ${PLAYGROUND_VERSION} (TypeScript)`);

// Initialize the contract references to avoid circular dependencies
initializeContract(Contract, ContractConfig, ContractAsync);

// start the plugin
StartPlugin(DefaultConfig());

// create a cancellable context that listens for kill signals
process.on('SIGINT', () => process.exit(0));
process.on('SIGTERM', () => process.exit(0));

import { Disk } from './Disk';

interface CartActionResponse {
    diskEntry: CartDiskEntry;
    length: number;
    total: number;
}

interface CartDiskEntry {
    disk: Disk;
    count: number;
    total: number;
}

interface CartSession {
    entries: CartDiskEntry[];
    total: number;
}

export type { CartActionResponse, CartDiskEntry, CartSession };

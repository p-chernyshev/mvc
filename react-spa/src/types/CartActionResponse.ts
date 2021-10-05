import { Disk } from './Disk';

export interface CartActionResponse {
    diskEntry: {
        disk: Disk;
        count: number;
        total: number;
    };
    length: number;
    total: number;
}

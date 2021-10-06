interface Disk {
    id: number;
    name: string;
    sizeGb: number;
    writeSpeedMbps: number;
    readSpeedMbps: number;
    price: number;
    type: DiskType;
    form: DiskForm;
}

enum DiskType {
    SSD = 0,
    HDD = 1,
}

const DISK_TYPE_DISPLAY = {
    [DiskType.SSD]: 'SSD',
    [DiskType.HDD]: 'HDD',
};

enum DiskForm {
    Sata25 = 0,
    Sata35 = 1,
    M2 = 2,
}

const DISK_FORM_DISPLAY = {
    [DiskForm.Sata25]: 'SATA 2.5"',
    [DiskForm.Sata35]: 'SATA 3.5"',
    [DiskForm.M2]: 'M.2',
};

export type { Disk };
export { DiskType, DISK_TYPE_DISPLAY, DiskForm, DISK_FORM_DISPLAY };

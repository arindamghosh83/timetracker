import { IWeeklyEffort, IEffort, IProject } from './models/effort.model';
export class DataProvider {
    public static Projects = [{
        description: "Hemophilia",
        active: true,
        id: 1
    },
    {
        description: "Time Tracker",
        active: true,
        id: 3
    }, {
        description: "T1DDexi",
        active: true,
        id: 2
    }, {
        description: "Garmin",
        active: true,
        id: 4
    },{
        description: "NASA",
        active: true,
        id: 5
    }, {
        description: "",
        active: true,
        id: 0
    }];

    public static SingleEffort = <IWeeklyEffort>{weekStartDate: "", weekEndDate: "", efforts: [{
        project: {
            description: "Hemophilia",
            id: 1,
            active: true
        },
        effortPercent: 0,
        isDeleted: false,
        selectableProjects: [],
        id:18
    }]};

    public static Efforts = [
        {
            weekStartDate: "9/30/2018",
            weekEndDate: "10/6/2018",
            efforts: [{
                project: {
                    description: "Hemophilia",
                    id: 1,
                    active: true
                },
                effortPercent: 0,
                isDeleted: false,
                selectableProjects: [],
                id:18
            },
            {
                project: {
                    description: "Time Tracker",
                    id: 3,
                    active: true
                },
                effortPercent: 0,
                isDeleted: false,
                selectableProjects: [],
                id: 17
            }
            ]
        },

        {
            weekStartDate: "9/23/2018",
            weekEndDate: "9/29/2018",
            efforts: [{
                project: {
                    description: "Hemophilia",
                    id: 1,
                    active: true
                },
                effortPercent: 50,
                isDeleted: false,
                selectableProjects: [],
                id: 16
            },
            {
                project: {
                    description: "Time Tracker",
                    id: 3,
                    active: true
                },
                effortPercent: 50,
                isDeleted: false,
                selectableProjects: [],
                id: 15
            }
            ]
        },
        {
            weekStartDate: "9/16/2018",
            weekEndDate: "9/22/2018",
            efforts: [{
                project: {
                    description: "Hemophilia",
                    id: 1,
                    active: true
                },
                effortPercent: 90,
                isDeleted: false,
                selectableProjects: [],
                id:5
            },
            {
                project: {
                    description: "T1DDexi",
                    id: 2,
                    active: true
                },
                effortPercent: 10,
                isDeleted: false,
                selectableProjects: [],
                id: 14
            }
            ]
        }
    ];
}
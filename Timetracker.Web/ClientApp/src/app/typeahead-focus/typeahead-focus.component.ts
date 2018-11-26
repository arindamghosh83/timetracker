import {
  Component,
  OnInit,
  ViewChild,
  Input,
  Output,
  EventEmitter
} from "@angular/core";
import { NgbTypeahead } from "@ng-bootstrap/ng-bootstrap";
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  map
} from "rxjs/operators";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { merge } from "rxjs/observable/merge";
import { IProject } from "../models/effort.model";
@Component({
  selector: "app-typeahead-focus",
  templateUrl: "./typeahead-focus.component.html",
  styleUrls: ["./typeahead-focus.component.scss"]
})
export class TypeaheadFocusComponent implements OnInit {
  model: any;
  @ViewChild("instance")
  instance: NgbTypeahead;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  @Input()
  dataSource: string[];
  @Input()
  disabledEffort: boolean;
  @Input()
  selectedEffortDescription: string;
  @Output()
  projectSelected = new EventEmitter<string>();
  search = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(
      debounceTime(200),
      distinctUntilChanged()
    );
    const clicksWithClosedPopup$ = this.click$.pipe(
      filter(() => !this.instance.isPopupOpen())
    );
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term =>
        (term === ""
          ? this.dataSource
          : this.dataSource.filter(v => {
              if (v) {
                return v.toLowerCase().indexOf(term.toLowerCase()) > -1;
              } else {
                return -1;
              }
            })
        ).slice(0, this.dataSource.length)
      )
    );
  };
  constructor() {}

  ngOnInit() {
    this.model = this.selectedEffortDescription;
    console.log("Data source", this.dataSource);
  }
  inputFormatter = value => value.description;
  onSelectChange = $event => {
    console.log("Selected project", $event);
    this.projectSelected.emit($event);
  };
}

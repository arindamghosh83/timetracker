import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { IEffort, IProject } from '../models/effort.model';

@Component({
  selector: 'app-effort-row',
  templateUrl: './effort-row.component.html',
  styleUrls: ['./effort-row.component.scss']
})
export class EfforRowComponent implements OnInit {
  @Input() effortrow: IEffort;
  @Input() effortindex: number;
  @Input() dropDownList: any;

  @Output()
  outgoingEffort: EventEmitter<IEffort> = new EventEmitter<IEffort>();

  constructor(
  ) {   
  }

  ngOnInit() { }

  exclude(effort: IEffort) {
    effort.isDeleted = true;
    this.outgoingEffort.emit(effort);
  }

  onEffortChange(changedValue: number) {
      this.effortrow.effortPercent = changedValue;
      this.outgoingEffort.emit(this.effortrow);
  }

  onSelectChange($event){
      this.effortrow.project.id = $event;
      this.outgoingEffort.emit(this.effortrow);
  }
}

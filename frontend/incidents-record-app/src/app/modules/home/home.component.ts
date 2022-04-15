import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  ELEMENT_DATA: any[] = [
    {
      number: '1',
      description: 'nice',
      Date: '05-05-2020',
      User: 'John',
      Significance: 'significance',
      SolvingDate: '03-03-2024',
      Verifies: 'Jess',
    },
    {
      number: '1',
      description: 'nice',
      Date: '05-05-2020',
      User: 'John',
      Significance: 'significance',
      SolvingDate: '03-03-2024',
      Verifies: 'Jess',
    },
    {
      number: '1',
      description: 'nice',
      Date: '05-05-2020',
      User: 'John',
      Significance: 'significance',
      SolvingDate: '03-03-2024',
      Verifies: 'Jess',
    },
    {
      number: '1',
      description: 'nice',
      Date: '05-05-2020',
      User: 'John',
      Significance: 'significance',
      SolvingDate: '03-03-2024',
      Verifies: 'Jess',
    },
    {
      number: '1',
      description: 'nice',
      Date: '05-05-2020',
      User: 'John',
      Significance: 'significance',
      SolvingDate: '03-03-2024',
      Verifies: 'Jess',
    },
  ];

  displayedColumns: string[] = [
    'number',
    'description',
    'date',
    'user',
    'significance',
    'solvingDate',
    'verifies',
    'actions',
  ];
  dataSource = this.ELEMENT_DATA;

  constructor() {}

  ngOnInit(): void {}
}
